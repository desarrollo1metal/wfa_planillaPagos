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
        Me.btnAceptar.Location = New System.Drawing.Point(24, 462)
        Me.btnAceptar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(147, 28)
        Me.btnAceptar.TabIndex = 57
        Me.btnAceptar.Tag = "1"
        Me.btnAceptar.Text = "Añadir"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(179, 462)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(147, 28)
        Me.btnCancel.TabIndex = 58
        Me.btnCancel.Tag = "Cancel"
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 74)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 17)
        Me.Label1.TabIndex = 59
        Me.Label1.Tag = ""
        Me.Label1.Text = "Código del menu"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 114)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 17)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Nombre del Menu"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 158)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 17)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "Tipo de Menu"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 194)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 17)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Menu Padre"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 236)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 17)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Posicion del Menu"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 281)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(120, 17)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Funcion del Menu"
        '
        'txtId
        '
        Me.txtId.Enabled = False
        Me.txtId.Location = New System.Drawing.Point(179, 70)
        Me.txtId.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtId.Name = "txtId"
        Me.txtId.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtId.Properties.Appearance.Options.UseFont = True
        Me.txtId.Size = New System.Drawing.Size(133, 20)
        Me.txtId.TabIndex = 65
        Me.txtId.Tag = "id"
        '
        'txtNom
        '
        Me.txtNom.Location = New System.Drawing.Point(179, 111)
        Me.txtNom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNom.Name = "txtNom"
        Me.txtNom.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtNom.Properties.Appearance.Options.UseFont = True
        Me.txtNom.Size = New System.Drawing.Size(385, 20)
        Me.txtNom.TabIndex = 66
        Me.txtNom.Tag = "nomMenu"
        '
        'txtOrden
        '
        Me.txtOrden.Location = New System.Drawing.Point(179, 233)
        Me.txtOrden.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtOrden.Name = "txtOrden"
        Me.txtOrden.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtOrden.Properties.Appearance.Options.UseFont = True
        Me.txtOrden.Size = New System.Drawing.Size(133, 20)
        Me.txtOrden.TabIndex = 67
        Me.txtOrden.Tag = "ordenMenu"
        '
        'txtFuncion
        '
        Me.txtFuncion.Location = New System.Drawing.Point(179, 277)
        Me.txtFuncion.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtFuncion.Name = "txtFuncion"
        Me.txtFuncion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtFuncion.Properties.Appearance.Options.UseFont = True
        Me.txtFuncion.Size = New System.Drawing.Size(236, 20)
        Me.txtFuncion.TabIndex = 68
        Me.txtFuncion.Tag = "funcion"
        '
        'cboTipoMenu
        '
        Me.cboTipoMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoMenu.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoMenu.FormattingEnabled = True
        Me.cboTipoMenu.Location = New System.Drawing.Point(179, 154)
        Me.cboTipoMenu.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboTipoMenu.Name = "cboTipoMenu"
        Me.cboTipoMenu.Size = New System.Drawing.Size(201, 21)
        Me.cboTipoMenu.TabIndex = 69
        Me.cboTipoMenu.Tag = "tipoMenu"
        '
        'cboMenuPadre
        '
        Me.cboMenuPadre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMenuPadre.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMenuPadre.FormattingEnabled = True
        Me.cboMenuPadre.Location = New System.Drawing.Point(179, 191)
        Me.cboMenuPadre.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboMenuPadre.Name = "cboMenuPadre"
        Me.cboMenuPadre.Size = New System.Drawing.Size(384, 21)
        Me.cboMenuPadre.TabIndex = 70
        Me.cboMenuPadre.Tag = "idMenuPadre"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(20, 394)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(68, 21)
        Me.CheckBox1.TabIndex = 71
        Me.CheckBox1.Tag = "activo"
        Me.CheckBox1.Text = "Activo"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 319)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 17)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Clase"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 356)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 17)
        Me.Label8.TabIndex = 73
        Me.Label8.Text = "Proyecto"
        '
        'TextEdit1
        '
        Me.TextEdit1.Location = New System.Drawing.Point(179, 354)
        Me.TextEdit1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.TextEdit1.Properties.Appearance.Options.UseFont = True
        Me.TextEdit1.Size = New System.Drawing.Size(236, 20)
        Me.TextEdit1.TabIndex = 74
        Me.TextEdit1.Tag = "proyecto"
        '
        'TextEdit2
        '
        Me.TextEdit2.Location = New System.Drawing.Point(179, 318)
        Me.TextEdit2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextEdit2.Name = "TextEdit2"
        Me.TextEdit2.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.TextEdit2.Properties.Appearance.Options.UseFont = True
        Me.TextEdit2.Size = New System.Drawing.Size(236, 20)
        Me.TextEdit2.TabIndex = 75
        Me.TextEdit2.Tag = "clase"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(439, 319)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(117, 21)
        Me.CheckBox2.TabIndex = 76
        Me.CheckBox2.Tag = "form"
        Me.CheckBox2.Text = "Es Formulario"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'btnExpMenu
        '
        Me.btnExpMenu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExpMenu.Location = New System.Drawing.Point(704, 462)
        Me.btnExpMenu.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnExpMenu.Name = "btnExpMenu"
        Me.btnExpMenu.Size = New System.Drawing.Size(183, 28)
        Me.btnExpMenu.TabIndex = 77
        Me.btnExpMenu.Tag = "sub_expDatosMenu"
        Me.btnExpMenu.Text = "Exportar Menu"
        Me.btnExpMenu.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(20, 425)
        Me.CheckBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(230, 21)
        Me.CheckBox3.TabIndex = 78
        Me.CheckBox3.Tag = "configSis"
        Me.CheckBox3.Text = "Menu de Configuracion Sistema"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'frmConfigMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(903, 512)
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
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
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
