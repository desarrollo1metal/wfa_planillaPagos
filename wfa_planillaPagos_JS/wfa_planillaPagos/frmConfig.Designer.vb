<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.chkEditTCPagML = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPrctDetrac = New DevExpress.XtraEditors.TextEdit()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.uls_CtaGancDC = New Util.uct_lstSeleccion()
        Me.uls_CtaPerdDC = New Util.uct_lstSeleccion()
        Me.chkAsTC = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.txtPrctDetrac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(12, 332)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(159, 23)
        Me.Button1.TabIndex = 57
        Me.Button1.Tag = "1"
        Me.Button1.Text = "Aceptar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(177, 332)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(159, 23)
        Me.Button2.TabIndex = 58
        Me.Button2.Tag = "Cancel"
        Me.Button2.Text = "Cancelar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(228, 13)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Configuraioón del Aplicativo para la compañia: "
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.ForeColor = System.Drawing.Color.White
        Me.lblCompany.Location = New System.Drawing.Point(243, 9)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(54, 13)
        Me.lblCompany.TabIndex = 60
        Me.lblCompany.Tag = "company"
        Me.lblCompany.Text = "Comapñia"
        '
        'chkEditTCPagML
        '
        Me.chkEditTCPagML.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkEditTCPagML.AutoSize = True
        Me.chkEditTCPagML.Enabled = False
        Me.chkEditTCPagML.Location = New System.Drawing.Point(16, 225)
        Me.chkEditTCPagML.Name = "chkEditTCPagML"
        Me.chkEditTCPagML.Size = New System.Drawing.Size(468, 17)
        Me.chkEditTCPagML.TabIndex = 62
        Me.chkEditTCPagML.Tag = "editTCPagML"
        Me.chkEditTCPagML.Text = "Permitir la modificacion del tipo de cambio para depositos en Soles de documentos" & _
    " en Dolares"
        Me.chkEditTCPagML.UseVisualStyleBackColor = True
        Me.chkEditTCPagML.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 13)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Porcentaje de Detracción "
        '
        'txtPrctDetrac
        '
        Me.txtPrctDetrac.Location = New System.Drawing.Point(150, 13)
        Me.txtPrctDetrac.Name = "txtPrctDetrac"
        Me.txtPrctDetrac.Size = New System.Drawing.Size(91, 20)
        Me.txtPrctDetrac.TabIndex = 64
        Me.txtPrctDetrac.Tag = "prctDetrac"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XtraTabControl1.Location = New System.Drawing.Point(12, 38)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage2
        Me.XtraTabControl1.Size = New System.Drawing.Size(876, 288)
        Me.XtraTabControl1.TabIndex = 65
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage2, Me.XtraTabPage1})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.txtPrctDetrac)
        Me.XtraTabPage1.Controls.Add(Me.chkEditTCPagML)
        Me.XtraTabPage1.Controls.Add(Me.Label3)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(870, 260)
        Me.XtraTabPage1.Text = "Detracciones"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.Label5)
        Me.XtraTabPage2.Controls.Add(Me.Label4)
        Me.XtraTabPage2.Controls.Add(Me.uls_CtaGancDC)
        Me.XtraTabPage2.Controls.Add(Me.uls_CtaPerdDC)
        Me.XtraTabPage2.Controls.Add(Me.chkAsTC)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(870, 260)
        Me.XtraTabPage2.Text = "Pll. Pagos Recibidos"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Cta. ganancia por dif. cambio"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Cta. pérdida por dif. cambio"
        '
        'uls_CtaGancDC
        '
        Me.uls_CtaGancDC.BackColor = System.Drawing.Color.White
        Me.uls_CtaGancDC.CampoTxt = "FormatCode"
        Me.uls_CtaGancDC.campoVal = "AcctCode"
        Me.uls_CtaGancDC.Location = New System.Drawing.Point(163, 69)
        Me.uls_CtaGancDC.mostrarCampo = "FormatCode"
        Me.uls_CtaGancDC.Name = "uls_CtaGancDC"
        Me.uls_CtaGancDC.Size = New System.Drawing.Size(182, 26)
        Me.uls_CtaGancDC.TabIndex = 2
        Me.uls_CtaGancDC.Tabla = "OACT"
        Me.uls_CtaGancDC.Tag = "ctaGanaciaTC"
        Me.uls_CtaGancDC.Texto = ""
        Me.uls_CtaGancDC.Value = Nothing
        '
        'uls_CtaPerdDC
        '
        Me.uls_CtaPerdDC.BackColor = System.Drawing.Color.White
        Me.uls_CtaPerdDC.CampoTxt = "FormatCode"
        Me.uls_CtaPerdDC.campoVal = "AcctCode"
        Me.uls_CtaPerdDC.Location = New System.Drawing.Point(163, 37)
        Me.uls_CtaPerdDC.mostrarCampo = "FormatCode"
        Me.uls_CtaPerdDC.Name = "uls_CtaPerdDC"
        Me.uls_CtaPerdDC.Size = New System.Drawing.Size(182, 26)
        Me.uls_CtaPerdDC.TabIndex = 1
        Me.uls_CtaPerdDC.Tabla = "OACT"
        Me.uls_CtaPerdDC.Tag = "ctaPerdidaTC"
        Me.uls_CtaPerdDC.Texto = ""
        Me.uls_CtaPerdDC.Value = Nothing
        '
        'chkAsTC
        '
        Me.chkAsTC.AutoSize = True
        Me.chkAsTC.Location = New System.Drawing.Point(14, 13)
        Me.chkAsTC.Name = "chkAsTC"
        Me.chkAsTC.Size = New System.Drawing.Size(331, 17)
        Me.chkAsTC.TabIndex = 0
        Me.chkAsTC.Tag = "creaAsTC"
        Me.chkAsTC.Text = "Creación automático del asiento de diferencia por tipo de cambio"
        Me.chkAsTC.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Gray
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblCompany)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(901, 32)
        Me.Panel1.TabIndex = 66
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(788, 7)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 18)
        Me.TextBox1.TabIndex = 62
        Me.TextBox1.Tag = "appVersion"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(734, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Version"
        '
        'frmConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 364)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmConfig"
        Me.OcultarBarra = True
        Me.Tag = "entConfig"
        Me.Text = "Configuracion de la Aplicacion"
        Me.Controls.SetChildIndex(Me.Button1, 0)
        Me.Controls.SetChildIndex(Me.Button2, 0)
        Me.Controls.SetChildIndex(Me.XtraTabControl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        CType(Me.txtPrctDetrac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage1.PerformLayout()
        Me.XtraTabPage2.ResumeLayout(False)
        Me.XtraTabPage2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents chkEditTCPagML As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPrctDetrac As DevExpress.XtraEditors.TextEdit
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents uls_CtaGancDC As Util.uct_lstSeleccion
    Friend WithEvents uls_CtaPerdDC As Util.uct_lstSeleccion
    Friend WithEvents chkAsTC As System.Windows.Forms.CheckBox
End Class
