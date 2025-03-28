<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGenPlanilla
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
        Me.ugcPlanilla = New Util.uct_gridConBusqueda()
        Me.ugcECBanco = New Util.uct_gridConBusqueda()
        Me.btnGenPlanilla = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.ugcECClientes = New Util.uct_gridConBusqueda()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ulsClientes = New Util.uct_lstSeleccion()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCtasBanco = New System.Windows.Forms.ComboBox()
        Me.btnBuscarClts = New System.Windows.Forms.Button()
        Me.btnBuscarEC = New System.Windows.Forms.Button()
        Me.btnAdcRegistro = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNroPla = New DevExpress.XtraEditors.TextEdit()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.DateEdit2 = New DevExpress.XtraEditors.DateEdit()
        Me.DateEdit3 = New DevExpress.XtraEditors.DateEdit()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboEstado = New System.Windows.Forms.ComboBox()
        Me.txtComent = New DevExpress.XtraEditors.TextEdit()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboTipoPla = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnExportar = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbPosBanco = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DateEdit4 = New DevExpress.XtraEditors.DateEdit()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.agregar2 = New System.Windows.Forms.Button()
        CType(Me.txtNroPla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit3.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit4.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ugcPlanilla
        '
        Me.ugcPlanilla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ugcPlanilla.BackColor = System.Drawing.Color.White
        Me.ugcPlanilla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ugcPlanilla.buscarPor = Nothing
        Me.ugcPlanilla.ColAlternaColor = "lineaNumAsg"
        Me.ugcPlanilla.ColElm = "lineaNumAsg"
        Me.ugcPlanilla.conFiltro = False
        Me.ugcPlanilla.conMenu = True
        Me.ugcPlanilla.DataSource = Nothing
        Me.ugcPlanilla.Location = New System.Drawing.Point(20, 489)
        Me.ugcPlanilla.Margin = New System.Windows.Forms.Padding(5)
        Me.ugcPlanilla.Name = "ugcPlanilla"
        Me.ugcPlanilla.ObjDetalle = Nothing
        Me.ugcPlanilla.PermitirOrden = False
        Me.ugcPlanilla.Size = New System.Drawing.Size(1739, 382)
        Me.ugcPlanilla.TabIndex = 2
        Me.ugcPlanilla.Tabla = "gmi_plaPagosDetalle"
        Me.ugcPlanilla.TablaId = "id"
        Me.ugcPlanilla.TablaId2 = Nothing
        Me.ugcPlanilla.Tag = "gmi_plaPagosDetalle"
        Me.ugcPlanilla.valorBusq = Nothing
        '
        'ugcECBanco
        '
        Me.ugcECBanco.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ugcECBanco.AutoScroll = True
        Me.ugcECBanco.BackColor = System.Drawing.Color.White
        Me.ugcECBanco.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ugcECBanco.buscarPor = "Fecha"
        Me.ugcECBanco.ColAlternaColor = ""
        Me.ugcECBanco.ColElm = Nothing
        Me.ugcECBanco.conFiltro = True
        Me.ugcECBanco.conMenu = False
        Me.ugcECBanco.DataSource = Nothing
        Me.ugcECBanco.Location = New System.Drawing.Point(928, 96)
        Me.ugcECBanco.Margin = New System.Windows.Forms.Padding(5)
        Me.ugcECBanco.Name = "ugcECBanco"
        Me.ugcECBanco.ObjDetalle = Nothing
        Me.ugcECBanco.PermitirOrden = True
        Me.ugcECBanco.Size = New System.Drawing.Size(831, 338)
        Me.ugcECBanco.TabIndex = 1
        Me.ugcECBanco.Tabla = Nothing
        Me.ugcECBanco.TablaId = "id"
        Me.ugcECBanco.TablaId2 = Nothing
        Me.ugcECBanco.Tag = "ECBanco"
        Me.ugcECBanco.valorBusq = Nothing
        '
        'btnGenPlanilla
        '
        Me.btnGenPlanilla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGenPlanilla.Location = New System.Drawing.Point(20, 879)
        Me.btnGenPlanilla.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGenPlanilla.Name = "btnGenPlanilla"
        Me.btnGenPlanilla.Size = New System.Drawing.Size(229, 28)
        Me.btnGenPlanilla.TabIndex = 3
        Me.btnGenPlanilla.Tag = "1"
        Me.btnGenPlanilla.Text = "Añadir"
        Me.btnGenPlanilla.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Location = New System.Drawing.Point(253, 879)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(224, 28)
        Me.btnCancelar.TabIndex = 4
        Me.btnCancelar.Tag = "Cancel"
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'ugcECClientes
        '
        Me.ugcECClientes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ugcECClientes.BackColor = System.Drawing.Color.White
        Me.ugcECClientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ugcECClientes.buscarPor = ""
        Me.ugcECClientes.ColAlternaColor = ""
        Me.ugcECClientes.ColElm = Nothing
        Me.ugcECClientes.conFiltro = True
        Me.ugcECClientes.conMenu = False
        Me.ugcECClientes.DataSource = Nothing
        Me.ugcECClientes.Location = New System.Drawing.Point(16, 96)
        Me.ugcECClientes.Margin = New System.Windows.Forms.Padding(5)
        Me.ugcECClientes.Name = "ugcECClientes"
        Me.ugcECClientes.ObjDetalle = Nothing
        Me.ugcECClientes.PermitirOrden = True
        Me.ugcECClientes.Size = New System.Drawing.Size(903, 338)
        Me.ugcECClientes.TabIndex = 0
        Me.ugcECClientes.Tabla = "gmi_plaPagosDetalle"
        Me.ugcECClientes.TablaId = "Id_Doc"
        Me.ugcECClientes.TablaId2 = "Line_ID"
        Me.ugcECClientes.Tag = "ECClientes"
        Me.ugcECClientes.valorBusq = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 64)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(157, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Documentos por cobrar"
        '
        'ulsClientes
        '
        Me.ulsClientes.BackColor = System.Drawing.Color.White
        Me.ulsClientes.CampoTxt = "CardName"
        Me.ulsClientes.campoVal = "CardCode"
        Me.ulsClientes.Location = New System.Drawing.Point(207, 60)
        Me.ulsClientes.Margin = New System.Windows.Forms.Padding(5)
        Me.ulsClientes.mostrarCampo = "CardCode"
        Me.ulsClientes.Name = "ulsClientes"
        Me.ulsClientes.Size = New System.Drawing.Size(243, 32)
        Me.ulsClientes.TabIndex = 6
        Me.ulsClientes.Tabla = "OCRD"
        Me.ulsClientes.Tag = "ulsClientes"
        Me.ulsClientes.Texto = ""
        Me.ulsClientes.Value = Nothing
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(887, 43)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(165, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Estado de Cuenta Banco"
        '
        'cboCtasBanco
        '
        Me.cboCtasBanco.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboCtasBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCtasBanco.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCtasBanco.FormattingEnabled = True
        Me.cboCtasBanco.Location = New System.Drawing.Point(891, 63)
        Me.cboCtasBanco.Margin = New System.Windows.Forms.Padding(4)
        Me.cboCtasBanco.Name = "cboCtasBanco"
        Me.cboCtasBanco.Size = New System.Drawing.Size(460, 21)
        Me.cboCtasBanco.TabIndex = 8
        Me.cboCtasBanco.Tag = "CtasBanco"
        '
        'btnBuscarClts
        '
        Me.btnBuscarClts.Location = New System.Drawing.Point(531, 60)
        Me.btnBuscarClts.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBuscarClts.Name = "btnBuscarClts"
        Me.btnBuscarClts.Size = New System.Drawing.Size(207, 28)
        Me.btnBuscarClts.TabIndex = 9
        Me.btnBuscarClts.Tag = "sub_cargarUGCClientes"
        Me.btnBuscarClts.Text = "Buscar"
        Me.btnBuscarClts.UseVisualStyleBackColor = True
        '
        'btnBuscarEC
        '
        Me.btnBuscarEC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBuscarEC.Location = New System.Drawing.Point(1557, 64)
        Me.btnBuscarEC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBuscarEC.Name = "btnBuscarEC"
        Me.btnBuscarEC.Size = New System.Drawing.Size(207, 28)
        Me.btnBuscarEC.TabIndex = 10
        Me.btnBuscarEC.Tag = "sub_cargarUGCEstadoCuenta"
        Me.btnBuscarEC.Text = "Buscar"
        Me.btnBuscarEC.UseVisualStyleBackColor = True
        '
        'btnAdcRegistro
        '
        Me.btnAdcRegistro.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdcRegistro.Location = New System.Drawing.Point(1616, 442)
        Me.btnAdcRegistro.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAdcRegistro.Name = "btnAdcRegistro"
        Me.btnAdcRegistro.Size = New System.Drawing.Size(147, 38)
        Me.btnAdcRegistro.TabIndex = 11
        Me.btnAdcRegistro.Tag = "sub_adicionarRegistro"
        Me.btnAdcRegistro.Text = "Adicionar Registro"
        Me.btnAdcRegistro.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 438)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 17)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Nro. de Planilla"
        '
        'txtNroPla
        '
        Me.txtNroPla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtNroPla.Enabled = False
        Me.txtNroPla.Location = New System.Drawing.Point(27, 458)
        Me.txtNroPla.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNroPla.Name = "txtNroPla"
        Me.txtNroPla.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtNroPla.Properties.Appearance.Options.UseFont = True
        Me.txtNroPla.Size = New System.Drawing.Size(157, 20)
        Me.txtNroPla.TabIndex = 13
        Me.txtNroPla.Tag = "id"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(188, 438)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 17)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Fecha Creacion"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(329, 438)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(134, 17)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Fecha Actualizacion"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(475, 438)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 17)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Fecha Proceso"
        '
        'DateEdit1
        '
        Me.DateEdit1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.Enabled = False
        Me.DateEdit1.Location = New System.Drawing.Point(192, 458)
        Me.DateEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.DateEdit1.Properties.Appearance.Options.UseFont = True
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Size = New System.Drawing.Size(133, 20)
        Me.DateEdit1.TabIndex = 18
        Me.DateEdit1.Tag = "FechaCrea"
        '
        'DateEdit2
        '
        Me.DateEdit2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DateEdit2.EditValue = Nothing
        Me.DateEdit2.Enabled = False
        Me.DateEdit2.Location = New System.Drawing.Point(333, 458)
        Me.DateEdit2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateEdit2.Name = "DateEdit2"
        Me.DateEdit2.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.DateEdit2.Properties.Appearance.Options.UseFont = True
        Me.DateEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit2.Size = New System.Drawing.Size(133, 20)
        Me.DateEdit2.TabIndex = 19
        Me.DateEdit2.Tag = "FechaAct"
        '
        'DateEdit3
        '
        Me.DateEdit3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DateEdit3.EditValue = Nothing
        Me.DateEdit3.Enabled = False
        Me.DateEdit3.Location = New System.Drawing.Point(475, 458)
        Me.DateEdit3.Margin = New System.Windows.Forms.Padding(4)
        Me.DateEdit3.Name = "DateEdit3"
        Me.DateEdit3.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.DateEdit3.Properties.Appearance.Options.UseFont = True
        Me.DateEdit3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit3.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit3.Size = New System.Drawing.Size(133, 20)
        Me.DateEdit3.TabIndex = 20
        Me.DateEdit3.Tag = "FechaPrcs"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(619, 438)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 17)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Estado"
        '
        'cboEstado
        '
        Me.cboEstado.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEstado.Enabled = False
        Me.cboEstado.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEstado.FormattingEnabled = True
        Me.cboEstado.Location = New System.Drawing.Point(616, 458)
        Me.cboEstado.Margin = New System.Windows.Forms.Padding(4)
        Me.cboEstado.Name = "cboEstado"
        Me.cboEstado.Size = New System.Drawing.Size(160, 21)
        Me.cboEstado.TabIndex = 22
        Me.cboEstado.Tag = "estado"
        '
        'txtComent
        '
        Me.txtComent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtComent.Location = New System.Drawing.Point(928, 458)
        Me.txtComent.Margin = New System.Windows.Forms.Padding(4)
        Me.txtComent.Name = "txtComent"
        Me.txtComent.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtComent.Properties.Appearance.Options.UseFont = True
        Me.txtComent.Size = New System.Drawing.Size(511, 20)
        Me.txtComent.TabIndex = 56
        Me.txtComent.Tag = "comentario"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(924, 438)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 17)
        Me.Label7.TabIndex = 57
        Me.Label7.Text = "Comentario"
        '
        'cboTipoPla
        '
        Me.cboTipoPla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboTipoPla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoPla.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoPla.FormattingEnabled = True
        Me.cboTipoPla.Location = New System.Drawing.Point(1447, 458)
        Me.cboTipoPla.Margin = New System.Windows.Forms.Padding(4)
        Me.cboTipoPla.Name = "cboTipoPla"
        Me.cboTipoPla.Size = New System.Drawing.Size(160, 21)
        Me.cboTipoPla.TabIndex = 58
        Me.cboTipoPla.Tag = "tipoPla"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(1443, 438)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(105, 17)
        Me.Label9.TabIndex = 59
        Me.Label9.Text = "Tipo de Planilla"
        '
        'btnExportar
        '
        Me.btnExportar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportar.Location = New System.Drawing.Point(480, 494)
        Me.btnExportar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExportar.Name = "btnExportar"
        Me.btnExportar.Size = New System.Drawing.Size(229, 28)
        Me.btnExportar.TabIndex = 60
        Me.btnExportar.Tag = "sub_ExportarExcel"
        Me.btnExportar.Text = "Exportar"
        Me.btnExportar.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(1356, 43)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(135, 17)
        Me.Label10.TabIndex = 61
        Me.Label10.Text = "Número de Posición"
        '
        'cmbPosBanco
        '
        Me.cmbPosBanco.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbPosBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPosBanco.FormattingEnabled = True
        Me.cmbPosBanco.Location = New System.Drawing.Point(1360, 60)
        Me.cmbPosBanco.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPosBanco.Name = "cmbPosBanco"
        Me.cmbPosBanco.Size = New System.Drawing.Size(188, 24)
        Me.cmbPosBanco.TabIndex = 62
        Me.cmbPosBanco.Tag = "PosBanco"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(785, 439)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 17)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Fecha Pago"
        '
        'DateEdit4
        '
        Me.DateEdit4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DateEdit4.EditValue = Nothing
        Me.DateEdit4.Location = New System.Drawing.Point(785, 459)
        Me.DateEdit4.Margin = New System.Windows.Forms.Padding(4)
        Me.DateEdit4.Name = "DateEdit4"
        Me.DateEdit4.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.DateEdit4.Properties.Appearance.Options.UseFont = True
        Me.DateEdit4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit4.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit4.Size = New System.Drawing.Size(133, 20)
        Me.DateEdit4.TabIndex = 20
        Me.DateEdit4.Tag = "FechaDocumento"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(1557, 30)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(203, 30)
        Me.Button1.TabIndex = 63
        Me.Button1.Tag = "sub_ExportarExcelDespositoCz"
        Me.Button1.Text = "Exportar Deposito"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'agregar2
        '
        Me.agregar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.agregar2.Location = New System.Drawing.Point(1632, 102)
        Me.agregar2.Margin = New System.Windows.Forms.Padding(4)
        Me.agregar2.Name = "agregar2"
        Me.agregar2.Size = New System.Drawing.Size(120, 28)
        Me.agregar2.TabIndex = 65
        Me.agregar2.Tag = "distribuir"
        Me.agregar2.Text = "Distribuir Pr"
        Me.agregar2.UseVisualStyleBackColor = True
        '
        'frmGenPlanilla
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1801, 912)
        Me.Controls.Add(Me.agregar2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmbPosBanco)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btnExportar)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboTipoPla)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtComent)
        Me.Controls.Add(Me.cboEstado)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.DateEdit4)
        Me.Controls.Add(Me.DateEdit3)
        Me.Controls.Add(Me.DateEdit2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.DateEdit1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtNroPla)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnAdcRegistro)
        Me.Controls.Add(Me.btnBuscarEC)
        Me.Controls.Add(Me.btnBuscarClts)
        Me.Controls.Add(Me.cboCtasBanco)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ulsClientes)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ugcECClientes)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGenPlanilla)
        Me.Controls.Add(Me.ugcPlanilla)
        Me.Controls.Add(Me.ugcECBanco)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmGenPlanilla"
        Me.Tag = "entPlanilla"
        Me.Text = "Generar Planilla"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.ugcECBanco, 0)
        Me.Controls.SetChildIndex(Me.ugcPlanilla, 0)
        Me.Controls.SetChildIndex(Me.btnGenPlanilla, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.ugcECClientes, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.ulsClientes, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.cboCtasBanco, 0)
        Me.Controls.SetChildIndex(Me.btnBuscarClts, 0)
        Me.Controls.SetChildIndex(Me.btnBuscarEC, 0)
        Me.Controls.SetChildIndex(Me.btnAdcRegistro, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtNroPla, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.DateEdit1, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.DateEdit2, 0)
        Me.Controls.SetChildIndex(Me.DateEdit3, 0)
        Me.Controls.SetChildIndex(Me.DateEdit4, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.cboEstado, 0)
        Me.Controls.SetChildIndex(Me.txtComent, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.cboTipoPla, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.btnExportar, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.cmbPosBanco, 0)
        Me.Controls.SetChildIndex(Me.Button1, 0)
        Me.Controls.SetChildIndex(Me.agregar2, 0)
        CType(Me.txtNroPla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit3.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit4.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ugcECBanco As Util.uct_gridConBusqueda
    Friend WithEvents ugcPlanilla As Util.uct_gridConBusqueda
    Friend WithEvents btnGenPlanilla As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents ugcECClientes As Util.uct_gridConBusqueda
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ulsClientes As Util.uct_lstSeleccion
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCtasBanco As System.Windows.Forms.ComboBox
    Friend WithEvents btnBuscarClts As System.Windows.Forms.Button
    Friend WithEvents btnBuscarEC As System.Windows.Forms.Button
    Friend WithEvents btnAdcRegistro As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNroPla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateEdit2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateEdit3 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboEstado As System.Windows.Forms.ComboBox
    Friend WithEvents txtComent As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboTipoPla As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnExportar As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbPosBanco As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DateEdit4 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents agregar2 As System.Windows.Forms.Button
End Class
