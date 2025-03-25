<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCargarEC
    Inherits Util.frmComun

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.txtRuta = New DevExpress.XtraEditors.TextEdit()
        Me.btnMostrar = New System.Windows.Forms.Button()
        Me.btnImportar = New System.Windows.Forms.Button()
        Me.btnECenPLa = New System.Windows.Forms.Button()
        Me.btnConsultEC = New System.Windows.Forms.Button()
        Me.ugcEC = New Util.uct_gridConBusqueda()
        Me.chkRegPll = New System.Windows.Forms.CheckBox()
        CType(Me.txtRuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(12, 12)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(115, 22)
        Me.btnBuscar.TabIndex = 1
        Me.btnBuscar.Tag = "sub_mostrarRuta"
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'txtRuta
        '
        Me.txtRuta.Location = New System.Drawing.Point(133, 14)
        Me.txtRuta.Name = "txtRuta"
        Me.txtRuta.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRuta.Properties.Appearance.Options.UseFont = True
        Me.txtRuta.Size = New System.Drawing.Size(435, 20)
        Me.txtRuta.TabIndex = 2
        Me.txtRuta.Tag = "rutaEC"
        '
        'btnMostrar
        '
        Me.btnMostrar.Location = New System.Drawing.Point(574, 11)
        Me.btnMostrar.Name = "btnMostrar"
        Me.btnMostrar.Size = New System.Drawing.Size(126, 23)
        Me.btnMostrar.TabIndex = 3
        Me.btnMostrar.Tag = "sub_mostrarEC"
        Me.btnMostrar.Text = "Mostrar"
        Me.btnMostrar.UseVisualStyleBackColor = True
        '
        'btnImportar
        '
        Me.btnImportar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImportar.Location = New System.Drawing.Point(1098, 12)
        Me.btnImportar.Name = "btnImportar"
        Me.btnImportar.Size = New System.Drawing.Size(142, 23)
        Me.btnImportar.TabIndex = 4
        Me.btnImportar.Tag = "sub_importarEC"
        Me.btnImportar.Text = "Importar"
        Me.btnImportar.UseVisualStyleBackColor = True
        '
        'btnECenPLa
        '
        Me.btnECenPLa.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnECenPLa.Location = New System.Drawing.Point(1050, 487)
        Me.btnECenPLa.Name = "btnECenPLa"
        Me.btnECenPLa.Size = New System.Drawing.Size(190, 23)
        Me.btnECenPLa.TabIndex = 56
        Me.btnECenPLa.Tag = "sub_ECenPlanillas"
        Me.btnECenPLa.Text = "Estado de Cuenta en Planillas"
        Me.btnECenPLa.UseVisualStyleBackColor = True
        '
        'btnConsultEC
        '
        Me.btnConsultEC.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConsultEC.Location = New System.Drawing.Point(854, 487)
        Me.btnConsultEC.Name = "btnConsultEC"
        Me.btnConsultEC.Size = New System.Drawing.Size(190, 23)
        Me.btnConsultEC.TabIndex = 57
        Me.btnConsultEC.Tag = "sub_consultarEC"
        Me.btnConsultEC.Text = "Consultar Estado de Cuenta"
        Me.btnConsultEC.UseVisualStyleBackColor = True
        '
        'ugcEC
        '
        Me.ugcEC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ugcEC.BackColor = System.Drawing.Color.White
        Me.ugcEC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ugcEC.buscarPor = Nothing
        Me.ugcEC.ColAlternaColor = ""
        Me.ugcEC.ColElm = ""
        Me.ugcEC.conFiltro = False
        Me.ugcEC.conMenu = False
        Me.ugcEC.DataSource = Nothing
        Me.ugcEC.Location = New System.Drawing.Point(12, 41)
        Me.ugcEC.Name = "ugcEC"
        Me.ugcEC.ObjDetalle = Nothing
        Me.ugcEC.PermitirOrden = False
        Me.ugcEC.Size = New System.Drawing.Size(1228, 440)
        Me.ugcEC.TabIndex = 68
        Me.ugcEC.Tabla = ""
        Me.ugcEC.TablaId = ""
        Me.ugcEC.TablaId2 = Nothing
        Me.ugcEC.Tag = "infoEC"
        Me.ugcEC.valorBusq = Nothing
        '
        'chkRegPll
        '
        Me.chkRegPll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkRegPll.AutoSize = True
        Me.chkRegPll.Location = New System.Drawing.Point(854, 15)
        Me.chkRegPll.Name = "chkRegPll"
        Me.chkRegPll.Size = New System.Drawing.Size(228, 17)
        Me.chkRegPll.TabIndex = 69
        Me.chkRegPll.Tag = "regPll"
        Me.chkRegPll.Text = "Registros para Planilla de Pagos Recibidos"
        Me.chkRegPll.UseVisualStyleBackColor = True
        '
        'frmCargarEC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1252, 521)
        Me.Controls.Add(Me.chkRegPll)
        Me.Controls.Add(Me.ugcEC)
        Me.Controls.Add(Me.btnConsultEC)
        Me.Controls.Add(Me.btnECenPLa)
        Me.Controls.Add(Me.btnImportar)
        Me.Controls.Add(Me.btnMostrar)
        Me.Controls.Add(Me.txtRuta)
        Me.Controls.Add(Me.btnBuscar)
        Me.Name = "frmCargarEC"
        Me.Tag = ""
        Me.Text = "Cargar Estado de Cuenta"
        Me.Controls.SetChildIndex(Me.btnBuscar, 0)
        Me.Controls.SetChildIndex(Me.txtRuta, 0)
        Me.Controls.SetChildIndex(Me.btnMostrar, 0)
        Me.Controls.SetChildIndex(Me.btnImportar, 0)
        Me.Controls.SetChildIndex(Me.btnECenPLa, 0)
        Me.Controls.SetChildIndex(Me.btnConsultEC, 0)
        Me.Controls.SetChildIndex(Me.ugcEC, 0)
        Me.Controls.SetChildIndex(Me.chkRegPll, 0)
        CType(Me.txtRuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents txtRuta As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnMostrar As System.Windows.Forms.Button
    Friend WithEvents btnImportar As System.Windows.Forms.Button
    Friend WithEvents btnECenPLa As System.Windows.Forms.Button
    Friend WithEvents btnConsultEC As System.Windows.Forms.Button
    Friend WithEvents ugcEC As Util.uct_gridConBusqueda
    Friend WithEvents chkRegPll As System.Windows.Forms.CheckBox

End Class
