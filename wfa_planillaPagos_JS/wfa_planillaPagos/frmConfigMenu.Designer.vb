<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigMenu
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
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtId = New DevExpress.XtraEditors.TextEdit()
        Me.txtNom = New DevExpress.XtraEditors.TextEdit()
        Me.txtOrden = New DevExpress.XtraEditors.TextEdit()
        Me.txtFuncion = New DevExpress.XtraEditors.TextEdit()
        Me.cboTipoMenu = New System.Windows.Forms.ComboBox()
        Me.cboMenuPadre = New System.Windows.Forms.ComboBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit2 = New DevExpress.XtraEditors.TextEdit()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.btnExpMenu = New System.Windows.Forms.Button()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        CType(Me.txtId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrden.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFuncion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.Location = New System.Drawing.Point(18, 375)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(110, 23)
        Me.btnAceptar.TabIndex = 57
        Me.btnAceptar.Tag = "1"
        Me.btnAceptar.Text = "Añadir"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(134, 375)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(110, 23)
        Me.btnCancel.TabIndex = 58
        Me.btnCancel.Tag = "Cancel"
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 59
        Me.Label1.Tag = ""
        Me.Label1.Text = "Código del menu"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Nombre del Menu"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 128)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "Tipo de Menu"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 158)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Menu Padre"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 192)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 13)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Posicion del Menu"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 228)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 13)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Funcion del Menu"
        '
        'txtId
        '
        Me.txtId.Enabled = False
        Me.txtId.Location = New System.Drawing.Point(134, 57)
        Me.txtId.Name = "txtId"
        Me.txtId.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtId.Properties.Appearance.Options.UseFont = True
        Me.txtId.Size = New System.Drawing.Size(100, 18)
        Me.txtId.TabIndex = 65
        Me.txtId.Tag = "id"
        '
        'txtNom
        '
        Me.txtNom.Location = New System.Drawing.Point(134, 90)
        Me.txtNom.Name = "txtNom"
        Me.txtNom.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtNom.Properties.Appearance.Options.UseFont = True
        Me.txtNom.Size = New System.Drawing.Size(289, 18)
        Me.txtNom.TabIndex = 66
        Me.txtNom.Tag = "nomMenu"
        '
        'txtOrden
        '
        Me.txtOrden.Location = New System.Drawing.Point(134, 189)
        Me.txtOrden.Name = "txtOrden"
        Me.txtOrden.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtOrden.Properties.Appearance.Options.UseFont = True
        Me.txtOrden.Size = New System.Drawing.Size(100, 18)
        Me.txtOrden.TabIndex = 67
        Me.txtOrden.Tag = "ordenMenu"
        '
        'txtFuncion
        '
        Me.txtFuncion.Location = New System.Drawing.Point(134, 225)
        Me.txtFuncion.Name = "txtFuncion"
        Me.txtFuncion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtFuncion.Properties.Appearance.Options.UseFont = True
        Me.txtFuncion.Size = New System.Drawing.Size(177, 18)
        Me.txtFuncion.TabIndex = 68
        Me.txtFuncion.Tag = "funcion"
        '
        'cboTipoMenu
        '
        Me.cboTipoMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoMenu.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoMenu.FormattingEnabled = True
        Me.cboTipoMenu.Location = New System.Drawing.Point(134, 125)
        Me.cboTipoMenu.Name = "cboTipoMenu"
        Me.cboTipoMenu.Size = New System.Drawing.Size(152, 19)
        Me.cboTipoMenu.TabIndex = 69
        Me.cboTipoMenu.Tag = "tipoMenu"
        '
        'cboMenuPadre
        '
        Me.cboMenuPadre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMenuPadre.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMenuPadre.FormattingEnabled = True
        Me.cboMenuPadre.Location = New System.Drawing.Point(134, 155)
        Me.cboMenuPadre.Name = "cboMenuPadre"
        Me.cboMenuPadre.Size = New System.Drawing.Size(289, 19)
        Me.cboMenuPadre.TabIndex = 70
        Me.cboMenuPadre.Tag = "idMenuPadre"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(15, 320)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(56, 17)
        Me.CheckBox1.TabIndex = 71
        Me.CheckBox1.Tag = "activo"
        Me.CheckBox1.Text = "Activo"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 259)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 13)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Clase"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 289)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 73
        Me.Label8.Text = "Proyecto"
        '
        'TextEdit1
        '
        Me.TextEdit1.Location = New System.Drawing.Point(134, 288)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.TextEdit1.Properties.Appearance.Options.UseFont = True
        Me.TextEdit1.Size = New System.Drawing.Size(177, 18)
        Me.TextEdit1.TabIndex = 74
        Me.TextEdit1.Tag = "proyecto"
        '
        'TextEdit2
        '
        Me.TextEdit2.Location = New System.Drawing.Point(134, 258)
        Me.TextEdit2.Name = "TextEdit2"
        Me.TextEdit2.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.TextEdit2.Properties.Appearance.Options.UseFont = True
        Me.TextEdit2.Size = New System.Drawing.Size(177, 18)
        Me.TextEdit2.TabIndex = 75
        Me.TextEdit2.Tag = "clase"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(329, 259)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(89, 17)
        Me.CheckBox2.TabIndex = 76
        Me.CheckBox2.Tag = "form"
        Me.CheckBox2.Text = "Es Formulario"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'btnExpMenu
        '
        Me.btnExpMenu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExpMenu.Location = New System.Drawing.Point(528, 375)
        Me.btnExpMenu.Name = "btnExpMenu"
        Me.btnExpMenu.Size = New System.Drawing.Size(137, 23)
        Me.btnExpMenu.TabIndex = 77
        Me.btnExpMenu.Tag = "sub_expDatosMenu"
        Me.btnExpMenu.Text = "Exportar Menu"
        Me.btnExpMenu.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(15, 345)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(176, 17)
        Me.CheckBox3.TabIndex = 78
        Me.CheckBox3.Tag = "configSis"
        Me.CheckBox3.Text = "Menu de Configuracion Sistema"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'frmConfigMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(677, 416)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.btnExpMenu)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.TextEdit2)
        Me.Controls.Add(Me.TextEdit1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.cboMenuPadre)
        Me.Controls.Add(Me.cboTipoMenu)
        Me.Controls.Add(Me.txtFuncion)
        Me.Controls.Add(Me.txtOrden)
        Me.Controls.Add(Me.txtNom)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAceptar)
        Me.Name = "frmConfigMenu"
        Me.Tag = "entMenu"
        Me.Text = "Menu"
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.btnCancel, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.txtId, 0)
        Me.Controls.SetChildIndex(Me.txtNom, 0)
        Me.Controls.SetChildIndex(Me.txtOrden, 0)
        Me.Controls.SetChildIndex(Me.txtFuncion, 0)
        Me.Controls.SetChildIndex(Me.cboTipoMenu, 0)
        Me.Controls.SetChildIndex(Me.cboMenuPadre, 0)
        Me.Controls.SetChildIndex(Me.CheckBox1, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.TextEdit1, 0)
        Me.Controls.SetChildIndex(Me.TextEdit2, 0)
        Me.Controls.SetChildIndex(Me.CheckBox2, 0)
        Me.Controls.SetChildIndex(Me.btnExpMenu, 0)
        Me.Controls.SetChildIndex(Me.CheckBox3, 0)
        CType(Me.txtId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrden.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFuncion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNom As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtOrden As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtFuncion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboTipoMenu As System.Windows.Forms.ComboBox
    Friend WithEvents cboMenuPadre As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents btnExpMenu As System.Windows.Forms.Button
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
End Class
