Public Class frmReporte
    Inherits frmComun

    ' Atributos del formulario
    Private o_dtbRep As DataTable

    ' Propiedades
    Public Property DataTableReporte() As DataTable
        Get
            Return o_dtbRep
        End Get
        Set(ByVal value As DataTable)
            o_dtbRep = value
        End Set
    End Property

#Region "Inicializacion"

    ' Se asigna el objeto de negocio asociado al formulario
    Overrides Sub sub_iniObjNegocio()
        Try

            ' Se asigna el objeto de negocio asociado a este formulario
            o_objNegocio = New classReporte(Me)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Constructor"

    Public Sub New(ByVal po_dtb As DataTable, ByVal ps_titulo As String)

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        o_dtbRep = po_dtb
        Me.Text = ps_titulo

    End Sub

#End Region

    Private Sub frmReporte_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class