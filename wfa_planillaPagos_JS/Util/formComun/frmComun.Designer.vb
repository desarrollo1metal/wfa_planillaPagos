<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmComun
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmComun))
        Me.pnlBarraHerr = New System.Windows.Forms.Panel()
        Me.btnCancelar = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRecp = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBuscarOF = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNuevo = New DevExpress.XtraEditors.SimpleButton()
        Me.lblModo = New System.Windows.Forms.Label()
        Me.pnlBarraHerr.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBarraHerr
        '
        Me.pnlBarraHerr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBarraHerr.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlBarraHerr.Controls.Add(Me.btnCancelar)
        Me.pnlBarraHerr.Controls.Add(Me.btnRecp)
        Me.pnlBarraHerr.Controls.Add(Me.btnBuscarOF)
        Me.pnlBarraHerr.Controls.Add(Me.btnNuevo)
        Me.pnlBarraHerr.Location = New System.Drawing.Point(0, 0)
        Me.pnlBarraHerr.Name = "pnlBarraHerr"
        Me.pnlBarraHerr.Size = New System.Drawing.Size(899, 39)
        Me.pnlBarraHerr.TabIndex = 55
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(130, 3)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(38, 34)
        Me.btnCancelar.TabIndex = 51
        Me.btnCancelar.Tag = "sub_cancelar"
        Me.btnCancelar.ToolTip = "Cancelar"
        '
        'btnRecp
        '
        Me.btnRecp.Image = CType(resources.GetObject("btnRecp.Image"), System.Drawing.Image)
        Me.btnRecp.Location = New System.Drawing.Point(89, 3)
        Me.btnRecp.Name = "btnRecp"
        Me.btnRecp.Size = New System.Drawing.Size(38, 34)
        Me.btnRecp.TabIndex = 50
        Me.btnRecp.Tag = "sub_recuperarForm"
        Me.btnRecp.ToolTip = "Recuperar"
        '
        'btnBuscarOF
        '
        Me.btnBuscarOF.Image = CType(resources.GetObject("btnBuscarOF.Image"), System.Drawing.Image)
        Me.btnBuscarOF.Location = New System.Drawing.Point(46, 3)
        Me.btnBuscarOF.Name = "btnBuscarOF"
        Me.btnBuscarOF.Size = New System.Drawing.Size(38, 34)
        Me.btnBuscarOF.TabIndex = 25
        Me.btnBuscarOF.Tag = "sub_preparaBuscar"
        Me.btnBuscarOF.ToolTip = "Buscar"
        '
        'btnNuevo
        '
        Me.btnNuevo.Image = CType(resources.GetObject("btnNuevo.Image"), System.Drawing.Image)
        Me.btnNuevo.Location = New System.Drawing.Point(3, 3)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(38, 34)
        Me.btnNuevo.TabIndex = 26
        Me.btnNuevo.Tag = "sub_preparaAnadir"
        Me.btnNuevo.ToolTip = "Nuevo"
        '
        'lblModo
        '
        Me.lblModo.AutoSize = True
        Me.lblModo.Location = New System.Drawing.Point(875, 505)
        Me.lblModo.Name = "lblModo"
        Me.lblModo.Size = New System.Drawing.Size(13, 13)
        Me.lblModo.TabIndex = 56
        Me.lblModo.Tag = "Modo"
        Me.lblModo.Text = "0"
        Me.lblModo.Visible = False
        '
        'frmComun
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(900, 527)
        Me.Controls.Add(Me.lblModo)
        Me.Controls.Add(Me.pnlBarraHerr)
        Me.Name = "frmComun"
        Me.Text = "Formulario"
        Me.pnlBarraHerr.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlBarraHerr As System.Windows.Forms.Panel
    Friend WithEvents btnRecp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBuscarOF As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNuevo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblModo As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As DevExpress.XtraEditors.SimpleButton
End Class
