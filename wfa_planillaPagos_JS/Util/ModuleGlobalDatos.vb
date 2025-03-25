Imports System.Reflection
Imports System.IO
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports SendFileToServer
Imports OfficeOpenXml
Imports System.Runtime.Remoting
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Excel
Imports ExcelDataReader

Public Module ModuleGlobalDatos

    ' Variables globales para los datos de conexion
    Public s_dbServ As String
    Public s_dbUser As String
    Public s_dbPass As String
    Public s_SAPComp As String
    Public s_SAPUser As String
    Public s_SAPPass As String
    Public s_ipServ As String
    Public s_svrUsr As String
    Public s_svrPwd As String
    Public s_sysUsr As String
    Public s_sysPass As String
    Public sNumPos As String

    ' Constante privada
    Private S_NOMMODULO As String = "ModuleSQLComun"

    Public Sub sub_obtDatosConexion(ByVal po_entConn As entConn)
        Try

            ' Se obtiene los datos de conexion
            s_dbServ = po_entConn.dbServ
            s_dbUser = po_entConn.dbUser
            s_dbPass = po_entConn.dbPass
            s_SAPComp = po_entConn.SAPComp
            s_SAPUser = po_entConn.SAPUser
            s_SAPPass = po_entConn.SAPPass
            s_ipServ = po_entConn.ipServ
            s_svrUsr = po_entConn.ServerUser
            s_svrPwd = po_entConn.ServerPassword

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Function obj_esNuloBD(ByVal po_valor As Object, ByVal po_valorRem As Object) As Object
        Try

            ' Se verifica si el valor recibido es NULL de base de datos
            If IsDBNull(po_valor) = True Then
                Return po_valorRem
            Else
                Return po_valor
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obj_esNuloBD(ByVal po_valor As Object) As Object
        Try

            ' Se verifica si el valor recibido es NULL de base de datos
            If IsDBNull(po_valor) = True Then

                ' Se verifica el tipo de dato del valor
                Return ""

            Else
                Return po_valor
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function int_verExistEnDTB(ByVal po_dtb As DataTable, ByVal ps_nomCol As String, ByVal po_valor As Object) As Integer
        Try

            ' Se declara una variable que contendra el indice de la fila que contiene el valor recibido
            Dim li_indice As Integer = -1

            ' Se busca el objeto en el dataTable
            If Not po_dtb Is Nothing Then ' Se verifica que el DataTable no sea nulo
                If po_dtb.Rows.Count > 0 Then ' Se verifica que haya registros en el DataTable
                    For i As Integer = 0 To po_dtb.Rows.Count - 1 ' Se recorre las filas del DataTable

                        ' Se verifica si el valor de la fila actual para la columna indicada es igual al valor recibido
                        If po_dtb.Rows(i)(ps_nomCol) = po_valor Then

                            ' Se asigna el valor del indice
                            li_indice = i

                        End If

                    Next
                End If
            End If

            ' Se retorna el indice
            Return li_indice

        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Function obj_convertlistEnArr(ByVal po_lst As List(Of Object)) As Object()
        Try

            ' Se retorna la lista convertida en arreglo
            Return po_lst.ToArray

        Catch ex As Exception
            Return New Object() {}
        End Try
    End Function

    Public Function met_obtenerMetodo(ByVal po_objeto As Object, ByVal ps_nomMetod As String) As MethodInfo
        Try

            ' Se obtiene el metodo a partir del nombre
            Dim lo_method As MethodInfo = po_objeto.GetType.GetMethod(ps_nomMetod, BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)

            ' Se verifica si se obtuvo el metodo
            If lo_method Is Nothing Then
                MsgBox("No se pudo obtener el metodo <" & ps_nomMetod & ">.")
            End If

            ' Se retorna el metodo
            Return lo_method

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function met_obtenerMetodoPrivate(ByVal po_objeto As Object, ByVal ps_nomMetod As String) As MethodInfo
        Try

            ' Se obtiene el metodo a partir del nombre
            Dim lo_method As MethodInfo = po_objeto.GetType.GetMethod(ps_nomMetod, BindingFlags.NonPublic)

            ' Se retorna el metodo
            Return lo_method

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function int_obtPosObjProcesVentas(ByVal pi_objType As Integer)
        Try

            ' Se verifica la posicion del objeto en proceso de ventas
            Select Case pi_objType
                Case 23
                    Return 0
                Case 17
                    Return 1
                Case 15
                    Return 2
                Case 16
                    Return 3
                Case 13
                    Return 4
                Case 14
                    Return 5
                Case Else
                    Return -1
            End Select

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return -1
        End Try
    End Function

    Public Function dtb_obtDatosDesdeExcel(ByVal po_fileInfo As FileInfo) As DataTable
        Try
            Dim rp As DataGridView = New DataGridView
            Dim data As DataTable
            Using stream As FileStream = File.Open(po_fileInfo.FullName, FileMode.Open, FileAccess.Read)
                Using reader As IExcelDataReader = ExcelReaderFactory.CreateReader(stream)
                    Dim dataset = reader.AsDataSet(New ExcelDataSetConfiguration() With {
                        .ConfigureDataTable = Function(tableReader) New ExcelDataTableConfiguration() With {
                            .UseHeaderRow = True
                        }
                    })

                    data = dataset.Tables(0)
                End Using
            End Using


            Return data

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    '************************ ANTERIOR ************************

    'Public Function dtb_obtDatosDesdeExcel(ByVal po_fileInfo As FileInfo) As DataTable
    '    Try

    '        ' Se declara una variable para la cadena de conexion
    '        Dim ls_connStr As String = ""

    '        ' Se verifica la extension del archivo excel para obtener la cadena de conexion hacia el archivo
    '        Select Case po_fileInfo.Extension
    '            Case ".xls"
    '                ls_connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties='Excel 8.0;HDR={1}'"
    '            Case ".xlsx"
    '                ls_connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties='Excel 8.0;HDR={1}'"
    '        End Select

    '        ' Se reemplaza los valores correspondientes en la cadena de conexion
    '        ls_connStr = String.Format(ls_connStr, po_fileInfo.FullName, "Yes")

    '        ' Se declara una instancia de OleDbConnection
    '        Dim lo_connXls As New OleDbConnection(ls_connStr)

    '        ' Se declara un objeto de tipo OledDbCommand
    '        Dim lo_command As New OleDbCommand()

    '        ' Se declara un OleDbDataAdapter
    '        Dim lo_dataAdapter As New OleDbDataAdapter

    '        ' Se declara un dataTable que contendra los datos obtenidos desde Excel
    '        Dim lo_dtb As New DataTable

    '        ' Se indica la conexion al objeto Command
    '        lo_command.Connection = lo_connXls

    '        ' Se abre la conexion
    '        lo_connXls.Open()

    '        ' Se obtiene el esquema del archivo excel
    '        Dim lo_dtbEsquema As DataTable = lo_connXls.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

    '        ' Se obtiene el nombre de la primera pestaña
    '        Dim ls_nomPestania As String = lo_dtbEsquema.Rows(0)("TABLE_NAME").ToString

    '        ' Se cierra la conexion
    '        lo_connXls.Close()

    '        ' Se abre la conexión denuevo
    '        lo_connXls.Open()

    '        ' Se asigna el texto de consulta hacia la primera pestaña del excel
    '        lo_command.CommandText = "SELECT * FROM [" + ls_nomPestania + "]"

    '        ' Se asigna el comando al objeto DataAdapter
    '        lo_dataAdapter.SelectCommand = lo_command

    '        ' Se llena el dataTable con los datos de la primera pestaña del Excel
    '        lo_dataAdapter.Fill(lo_dtb)

    '        ' Se cierra la conexion
    '        lo_connXls.Close()

    '        ' Se retorna el dataTable
    '        Return lo_dtb

    '    Catch ex As Exception
    '        MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function bol_existColumnaDtb(ByVal po_dtb As DataTable, ByVal ps_nomColum As String) As Boolean
        Try

            ' Se obtiene la columna 
            Dim lo_colum As DataColumn = po_dtb.Columns.Item(ps_nomColum)

            ' Se verifica si se obtuvo la columna
            If Not lo_colum Is Nothing Then
                Return True
            End If

            ' Se verifica si se obtuvo la columna
            For Each lo_col As DataColumn In po_dtb.Columns

                ' Se verifica el nombre 
                If lo_col.ColumnName.Trim.ToLower = ps_nomColum.Trim.ToLower Then
                    Return True
                End If

            Next

            ' Si no se encontró la columna 
            Return False

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function sub_guardarDTBcmoTxtTabDelimtd(ByVal po_dtb As DataTable, ByVal ps_ruta As String, Optional ByVal ps_rutaFinal As String = "") As Boolean
        Try

            ' Se declara un StreamReader
            Dim lo_swrTXT As New StreamWriter(ps_ruta)

            ' Se declara una variable String que contendra las lineas a escribir
            Dim ls_strLine As String

            ' Se declara una variable de tipo caracter que contenga el espacio TAB
            Dim lc_chrTAB As Char = Chr(Keys.Tab)

            ' Se recorre el dataTable
            For Each lo_drRow As DataRow In po_dtb.Rows

                ' Se inicializa la variable de texto
                ls_strLine = ""

                ' Se recorre las columnas del dataTable
                For li_intCol As Integer = 0 To po_dtb.Columns.Count - 1

                    'Se verifica si es la ultima columna para no añadir el TAB
                    If li_intCol = po_dtb.Columns.Count - 1 Then
                        If TypeOf (lo_drRow(li_intCol)) Is DateTime Then
                            ls_strLine &= CType(lo_drRow(li_intCol), DateTime).ToString("yyyyMMdd")
                        Else
                            ls_strLine &= lo_drRow(li_intCol).ToString
                        End If
                    Else 'Sino, se añade el caracter TAB
                        If TypeOf (lo_drRow(li_intCol)) Is DateTime Then
                            ls_strLine &= CType(lo_drRow(li_intCol), DateTime).ToString("yyyyMMdd") & lc_chrTAB
                        Else
                            ls_strLine &= lo_drRow(li_intCol).ToString & lc_chrTAB
                        End If
                    End If
                Next

                ' Se añade la variable de texto al StreamWriter
                lo_swrTXT.WriteLine(ls_strLine)

            Next

            ' Se guarda los cambios y se cierra el StreamWriter
            lo_swrTXT.Flush()
            lo_swrTXT.Close()
            lo_swrTXT.Dispose()

            ' Se verifica si se recibio una ruta final para el archivo
            If ps_rutaFinal.Trim <> "" Then

                ' Se copia el archivo en la ruta final dentro del servidor en donde se encuentra la base de datos
                Send.SaveACopyfileToServer(ps_ruta, ps_rutaFinal, s_svrUsr, s_svrPwd)

            End If

            ' Si el proceso finalizo con exito, se retorn TRUE
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Public Function bol_guardarDtbEnXML(ByVal po_dtb As DataTable, ByVal ps_ruta As String) As Boolean
        Try

            ' Se genera un texto xml a partir del dataTable
            Dim lo_dsCab As New DataSet
            lo_dsCab.Merge(po_dtb)
            lo_dsCab.WriteXml(ps_ruta)

            ' Se retorna TRUE si no ocurrió ningun error
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Public Function dtb_obtenerDtbDeXML(ByVal ps_ruta As String) As DataTable
        Try

            ' Se crea un dataTable para los datos de cabecera
            Dim lo_dtb As New DataTable
            Dim lo_dsCab As New DataSet

            ' Se asigna los nobmres correspondientes a los dataTable
            lo_dtb.TableName = "dtbDesdeXML"

            ' Se asigna los datos obtenidos al formulario
            lo_dsCab.ReadXml(ps_ruta)

            ' Se obtiene los dataTables desde los dataSet
            If lo_dsCab.Tables.Count > 0 Then
                lo_dtb = lo_dsCab.Tables(0)
            Else
                MsgBox("No se obtuvo datos desde el XML <menu>.")
            End If

            ' Se retorna TRUE si no ocurrió ningun error
            Return lo_dtb

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obj_convertirATipo(ByVal po_valor As Object, ByVal po_type As Type) As Object
        Try

            ' Se verifica el tipo del objeto recibido
            If po_valor Is Nothing Then
                Return Nothing
            ElseIf po_valor.ToString.Trim = "" Then
                If po_type = GetType(String) Then
                    Return ""
                ElseIf po_type = GetType(Integer) Then
                    Return -1
                Else
                    Return Nothing
                End If
            ElseIf po_type = GetType(Integer) Then
                Return CInt(po_valor)
            ElseIf po_type = GetType(Int32) Then
                Return CType(po_valor, Int32)
            ElseIf po_type = GetType(Double) Then
                Return CDbl(po_valor)
            ElseIf po_type = GetType(Decimal) Then
                Return CType(po_valor, Decimal)
            ElseIf po_type = GetType(String) Then
                Return CStr(po_valor)
            ElseIf po_type = GetType(Date) Then
                Return CDate(po_valor)
            Else
                MsgBox(System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & "Tipo no encontrado. (" & po_type.Name & ")")
                Return po_valor
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return po_valor
        End Try
    End Function

    Public Function ann_obtenerAnotacion(ByVal po_objeto As Object, ByVal po_type As Type) As Attribute
        Try

            ' Se obtiene los atributos del objeto
            For Each lo_atr As Object In po_objeto.GetType.GetCustomAttributes(True)

                ' Se verifica el tipo del atributo y se compara con el parametro recibido
                If po_type = lo_atr.GetType Then
                    Return lo_atr
                End If

            Next

            ' Si no se encontro un atributo del tipo recibido
            Return Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ann_obtenerAnotacion(ByVal po_field As FieldInfo, ByVal po_type As Type) As Attribute
        Try

            ' Se obtiene los atributos del objeto
            For Each lo_atr As Object In po_field.GetCustomAttributes(True)

                ' Se verifica el tipo del atributo y se compara con el parametro recibido
                If po_type = lo_atr.GetType Then
                    Return lo_atr
                End If

            Next

            ' Si no se encontro un atributo del tipo recibido
            Return Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub sub_genExcelDesdeDataTable(ByVal po_dtb As DataTable, ByVal ps_nomReport As String)
        Try

            Dim strRutaFormato As String = Application.StartupPath & "\FormatosExcelReportes\reporteExcel.xlsx"
            Dim strRutaTemp As String = Path.GetTempPath & ps_nomReport & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"
            Dim fNewFile As New FileInfo(strRutaTemp)
            Dim fexistingFile As New FileInfo(strRutaFormato)

            Using MyExcel As New ExcelPackage(fexistingFile)
                Dim iExcelWS As ExcelWorksheet
                iExcelWS = MyExcel.Workbook.Worksheets("hoja1")
                'po_dtb.Columns.RemoveAt(0) : po_dtb.AcceptChanges()

                ' Se indica que los datos del reporte deben iniciar en la celda B5
                iExcelWS.Cells("B3").LoadFromDataTable(po_dtb, True)

                ' Se asigna el nombre del reporte a la segunda celda
                iExcelWS.Cells("A1").Value = ps_nomReport.ToUpper

                ' Se recorre las columnas de la cabecera para asignarles un estilo especficio
                For i As Integer = 0 To po_dtb.Columns.Count - 1

                    ' Se asigna el estilo a la celda
                    iExcelWS.Cells(3, 2 + i).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)

                Next

                ' Se asigna borde a las celdas
                'iExcelWS.Cells("B5:B" & po_dtb.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                'iExcelWS.Cells("D5:D" & po_dtb.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                'iExcelWS.Cells("F5:F" & po_dtb.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                'iExcelWS.Cells("H5:H" & po_dtb.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                'iExcelWS.Cells("J5:J" & po_dtb.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                'iExcelWS.Cells("L5:L" & po_dtb.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                'iExcelWS.Cells("M5:M" & po_dtb.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                'For i As Integer = 6 To po_dtb.Rows.Count + 4 Step 2
                '    iExcelWS.Cells("B" & i & ":M" & i).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                'Next

                ' Se graba el archivo
                MyExcel.SaveAs(fNewFile)

            End Using
            System.Diagnostics.Process.Start(strRutaTemp)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Function str_letraDesdeNro(ByVal li_num As Integer) As String
        Try

            If li_num > 0 AndAlso li_num < 27 Then
                Dim c As Char
                c = Convert.ToChar(li_num + 64)
                Return c
            Else
                Return ""
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ""
        End Try
    End Function

    Public Function obj_intentarCast(ByVal po_val As Object, ByVal po_type As Type) As Object
        Try
            Return CTypeDynamic(po_val, po_type)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function dte_obtFechaActual() As Date
        Try
            Return Now.Date
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function str_obtWinUsr() As String
        Try
            Return Environment.UserName
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function str_obtNomPC() As String
        Try
            Return System.Net.Dns.GetHostName()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function str_obtIpPC() As String
        Try
            Return System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList(1).ToString()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function str_obtValDeYesNoStr(ByVal ps_val As String) As String
        Try
            If ps_val.Trim.ToLower = "y" Then
                Return "Si"
            ElseIf ps_val.Trim.ToLower = "n" Then
                Return "No"
            Else
                Return ps_val
            End If
        Catch ex As Exception
            Return ps_val
        End Try
    End Function

    Public Enum enm_tipoMenu
        Titulo = 0
        Funcion = 1
    End Enum

    Public Function obj_obtObjeto(ByVal ps_proyecto As String, ByVal ps_clase As String, Optional ByVal po_form As Form = Nothing) As Object
        Try

            ' Se declara un objectHandle que obtenga la clase recibida
            Dim lo_objHdl As ObjectHandle

            ' Se declara un objeto para obtener el resultado
            Dim lo_objeto As Object

            ' Se verifica si se recibido un formulario como parametro
            If po_form Is Nothing Then
                lo_objHdl = Activator.CreateInstance(ps_proyecto, ps_proyecto & "." & ps_clase)

                ' Se verifica si se obtuvo el objetoHandle con los parametros recibidos
                If lo_objHdl Is Nothing Then
                    MsgBox("No se pudo obtener el ObjetHandle de la clase <" & ps_clase & "> del proyecto <" & ps_proyecto & ">")
                    Return Nothing
                End If

                ' Se obtiene el objeto
                lo_objeto = lo_objHdl.Unwrap

            Else

                ' Se obtiene el assembly con el nombre recibido
                Dim lo_assembly As Assembly = Assembly.Load(ps_proyecto)

                ' Se verifica si se obtuvo el tipo con los parametros recibidos
                If lo_assembly Is Nothing Then
                    MsgBox("No se pudo obtener el Assembly de la clase <" & ps_clase & "> del proyecto <" & ps_proyecto & ">")
                    Return Nothing
                End If

                ' Se obtiene el tipo con el nombre recibido
                Dim lo_type As Type = lo_assembly.GetType(ps_proyecto & "." & ps_clase)

                ' Se verifica si se obtuvo el tipo con los parametros recibidos
                If lo_type Is Nothing Then
                    MsgBox("No se pudo obtener el Tipo de la clase <" & ps_clase & "> del proyecto <" & ps_proyecto & ">")
                    Return Nothing
                End If

                ' Se obtiene el objeto de acuerdo al tipo obtenido
                lo_objeto = Activator.CreateInstance(lo_type, New Object() {po_form})
            End If

            ' Se verifica si se obtuvo el objeto con los parametros recibidos
            If lo_objeto Is Nothing Then
                MsgBox("No se pudo obtener el Objeto de la clase <" & ps_clase & "> del proyecto <" & ps_proyecto & ">")
                Return Nothing
            End If

            ' Se retorna el objeto
            Return lo_objeto

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function lon_obtVersionAPP() As Long
        Try

            ' Se obtiene el dato desde la carpeta Version
            Dim ls_version As String = str_obtDatoDesdeTXT(My.Application.Info.DirectoryPath & "\Version", "version.txt")

            ' Se verifica si se obtuvo el valor correspondiente a la version (8 caracteres)
            If ls_version.Trim.Length <> 12 Then
                MsgBox("La versión de la aplicación solo debe contar con ocho caracteres. Número de caracteres encontrados: " & ls_version.Trim.Length.ToString)
            End If

            ' Se verifica si la cadena corresponde a un valor numerico
            Dim ll_version As Long = CType(ls_version.Trim, Long)

            ' Se retorna la version
            Return ll_version

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return -1
        End Try
    End Function

    Public Function str_obtDatoDesdeTXT(ByVal ps_directorio As String, ByVal ps_archivo As String) As String
        Try

            ' Se declara una variable para el resultado
            Dim ls_res As String = ""

            ' Se obtiene el directorio donde se encuentran los archivos XML de configuracion de SAP Business One Client
            Dim lo_directory As New DirectoryInfo(ps_directorio)

            ' Se obtiene los archivos del directorio 
            Dim lo_files As FileInfo() = lo_directory.GetFiles

            ' Se recorre los archivos del directorio
            For Each lo_file As FileInfo In lo_files

                ' Se verifica el nombre del archivo
                If lo_file.Name.ToLower = ps_archivo.ToLower Then

                    ' Se declara una variable para verificar que solo se obtenga el dato de la primera linea
                    Dim li_contador As Integer = 1

                    ' Se lee el archivo
                    Using sr As New IO.StreamReader(lo_file.FullName)
                        While (Not sr.EndOfStream)
                            If li_contador = 1 Then
                                ls_res = sr.ReadLine
                            End If
                        End While
                    End Using

                End If

            Next

            ' Se verifica si se obtuvo algun valor
            If ls_res.Trim = "" Then
                MsgBox("No se obtuvo ningun dato en la primera linea del archivo <" & ps_directorio & ps_archivo & ">")
                Return ""
            End If

            ' Se muestra un mensaje de error que indique que no se encontro ningun dato
            Return ls_res

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ""
        End Try
    End Function

    Public Function bol_grabarValorEnTXT(ByVal ps_valor As String, ByVal ps_directorio As String, ByVal ps_archivo As String) As Boolean
        Try

            ' Se declara un StreamReader
            Dim lo_swrTXT As New StreamWriter(ps_directorio & "\" & ps_archivo)

            ' Se declara una variable de tipo caracter que contenga el espacio TAB
            Dim lc_chrTAB As Char = Chr(Keys.Tab)

            ' Se añade la variable de texto al StreamWriter
            lo_swrTXT.WriteLine(ps_valor)

            ' Se guarda los cambios y se cierra el StreamWriter
            lo_swrTXT.Flush()
            lo_swrTXT.Close()
            lo_swrTXT.Dispose()

            ' Si el proceso finalizo con exito, se retorn TRUE
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Public Function str_completasCerosIzq(pi_num As Integer, pi_longitud As Integer)
        Try

            ' Se verifica la longitud
            Return Right(StrDup(pi_longitud, "0") & pi_num.ToString, pi_longitud)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ""
        End Try
    End Function

#Region "Log"

    Public Sub sub_mostrarMensaje(ps_mensaje As String, ps_proy As String, ps_clase As String, ps_metodo As String, pi_tipoMsj As Integer, Optional pb_registrarLog As Boolean = True)
        Try

            If pi_tipoMsj = enm_tipoMsj.error_exc Then
                MessageBox.Show(ps_proy & "." & ps_clase & "." & ps_metodo & ": " & ps_mensaje, "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf pi_tipoMsj = enm_tipoMsj.error_val Or pi_tipoMsj = enm_tipoMsj.error_sap Or pi_tipoMsj = enm_tipoMsj.error_sis Then
                MessageBox.Show(ps_mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf pi_tipoMsj = enm_tipoMsj.info Then
                MessageBox.Show(ps_mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf pi_tipoMsj = enm_tipoMsj.exito Then
                MessageBox.Show(ps_mensaje, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ' Se verifica si se debe registrar Log para el mensaje actual
            If pb_registrarLog = False Then
                Exit Sub
            End If

            ' Se verifica el tipo de mensaje
            sub_registrarLog(ps_mensaje, ps_proy, ps_clase, ps_metodo, pi_tipoMsj)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_registrarLog(ps_mensaje As String, ps_proy As String, ps_clase As String, ps_metodo As String, pi_tipoMsj As Integer)
        Try

            ' Se declara una variable de texto para el resultado de la operacion
            Dim ls_resultado As String = ""

            ' Se declara un objeto de tipo Log
            Dim lo_log As New entLog

            ' Se obtiene la hora actual en numero entero
            Dim li_hora As Integer = Now.Hour.ToString & str_completasCerosIzq(Now.Minute, 2)

            ' Se asigna las propiedades al objeto
            lo_log.Proyecto = ps_proy
            lo_log.Clase = ps_clase
            lo_log.Metodo = ps_metodo
            lo_log.Mensaje = ps_mensaje
            lo_log.Tipo = pi_tipoMsj
            lo_log.Fecha = Now.Date
            lo_log.Hora = li_hora

            ' Se añade el objeto a la base de datos
            ls_resultado = lo_log.str_anadir

            ' Se verifica el resultado de la operacion
            If ls_resultado.Trim <> "" Then
                MsgBox("No se pudo grabar el log: " & ls_resultado)
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "sysUsr"

    Public Function str_encriptar(ps_cadena As String) As String
        Try

            ' Se obtiene el texto encriptado
            Dim wrapper As New Simple3Des(ps_cadena)
            Dim cipherText As String = wrapper.EncryptData(ps_cadena)

            ' Se retorna el texto encriptado
            Return cipherText

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ""
        End Try
    End Function

    Public Function str_desencriptar(ps_cadena As String) As String
        Try

            ' Se desencripta el texto recibido
            Dim wrapper As New Simple3Des(ps_cadena)
            Dim plainText As String = wrapper.DecryptData(ps_cadena)

            ' Se retorna el texto
            Return plainText

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ""
        End Try
    End Function

    Public Function str_actPasswordUsr(ps_codigo As String, ps_pass As String) As String
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_actPasswordUsr '" & ps_codigo & "', '" & ps_pass & "'"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.str_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, S_NOMMODULO, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ex.Message
        End Try
    End Function

    Public Function int_verPassUsr(ps_codigo As String, ps_pass As String) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verPassUsr '" & ps_codigo & "', '" & ps_pass & "'"

            ' Se ejecuta la consulta
            Return CInt(ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql).Rows(0)(0))

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, S_NOMMODULO, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

#End Region

End Module
