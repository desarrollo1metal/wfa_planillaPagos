<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcesarPlanilla
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
        Me.cboEstado = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DateEdit3 = New DevExpress.XtraEditors.DateEdit()
        Me.DateEdit2 = New DevExpress.XtraEditors.DateEdit()
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNroPla = New DevExpress.XtraEditors.TextEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ugcPlanilla = New Util.uct_gridConBusqueda()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCancelPla = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtComent = New DevExpress.XtraEditors.TextEdit()
        Me.btnExpPlaCerr = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DateEdit3.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNroPla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboEstado
        '
        Me.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEstado.Enabled = False
        Me.cboEstado.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEstado.FormattingEnabled = True
        Me.cboEstado.Location = New System.Drawing.Point(609, 85)
        Me.cboEstado.Margin = New System.Windows.Forms.Padding(4)
        Me.cboEstado.Name = "cboEstado"
        Me.cboEstado.Size = New System.Drawing.Size(160, 21)
        Me.cboEstado.TabIndex = 65
        Me.cboEstado.Tag = "estado"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(612, 65)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 17)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Estado"
        '
        'DateEdit3
        '
        Me.DateEdit3.EditValue = Nothing
        Me.DateEdit3.Enabled = False
        Me.DateEdit3.Location = New System.Drawing.Point(468, 85)
        Me.DateEdit3.Margin = New System.Windows.Forms.Padding(4)
        Me.DateEdit3.Name = "DateEdit3"
        Me.DateEdit3.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.DateEdit3.Properties.Appearance.Options.UseFont = True
        Me.DateEdit3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit3.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit3.Size = New System.Drawing.Size(133, 20)
        Me.DateEdit3.TabIndex = 63
        Me.DateEdit3.Tag = "FechaPrcs"
        '
        'DateEdit2
        '
        Me.DateEdit2.EditValue = Nothing
        Me.DateEdit2.Enabled = False
        Me.DateEdit2.Location = New System.Drawing.Point(327, 85)
        Me.DateEdit2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateEdit2.Name = "DateEdit2"
        Me.DateEdit2.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.DateEdit2.Properties.Appearance.Options.UseFont = True
        Me.DateEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit2.Size = New System.Drawing.Size(133, 20)
        Me.DateEdit2.TabIndex = 62
        Me.DateEdit2.Tag = "FechaAct"
        '
        'DateEdit1
        '
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.Enabled = False
        Me.DateEdit1.Location = New System.Drawing.Point(185, 85)
        Me.DateEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.DateEdit1.Properties.Appearance.Options.UseFont = True
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Size = New System.Drawing.Size(133, 20)
        Me.DateEdit1.TabIndex = 61
        Me.DateEdit1.Tag = "FechaCrea"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(468, 65)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 17)
        Me.Label6.TabIndex = 60
        Me.Label6.Text = "Fecha Proceso"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(323, 65)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(134, 17)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "Fecha Actualizacion"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(181, 65)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 17)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Fecha Creacion"
        '
        'txtNroPla
        '
        Me.txtNroPla.Enabled = False
        Me.txtNroPla.Location = New System.Drawing.Point(20, 85)
        Me.txtNroPla.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNroPla.Name = "txtNroPla"
        Me.txtNroPla.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtNroPla.Properties.Appearance.Options.UseFont = True
        Me.txtNroPla.Size = New System.Drawing.Size(157, 20)
        Me.txtNroPla.TabIndex = 57
        Me.txtNroPla.Tag = "id"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 65)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 17)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Nro. de Planilla"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1811, 50)
        Me.Panel1.TabIndex = 66
        '
        'ugcPlanilla
        '
        Me.ugcPlanilla.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ugcPlanilla.BackColor = System.Drawing.Color.White
        Me.ugcPlanilla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ugcPlanilla.buscarPor = Nothing
        Me.ugcPlanilla.ColAlternaColor = "lineaNumAsg"
        Me.ugcPlanilla.ColElm = ""
        Me.ugcPlanilla.conFiltro = False
        Me.ugcPlanilla.conMenu = False
        Me.ugcPlanilla.DataSource = Nothing
        Me.ugcPlanilla.Location = New System.Drawing.Point(16, 127)
        Me.ugcPlanilla.Margin = New System.Windows.Forms.Padding(5)
        Me.ugcPlanilla.Name = "ugcPlanilla"
        Me.ugcPlanilla.ObjDetalle = Nothing
        Me.ugcPlanilla.PermitirOrden = False
        Me.ugcPlanilla.Size = New System.Drawing.Size(1779, 466)
        Me.ugcPlanilla.TabIndex = 67
        Me.ugcPlanilla.Tabla = "gmi_plaPagosDetalle"
        Me.ugcPlanilla.TablaId = "id"
        Me.ugcPlanilla.TablaId2 = Nothing
        Me.ugcPlanilla.Tag = "gmi_plaPagosDetalle"
        Me.ugcPlanilla.valorBusq = Nothing
        '
        'btnProcesar
        '
        Me.btnProcesar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnProcesar.Location = New System.Drawing.Point(20, 601)
        Me.btnProcesar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(146, 39)
        Me.btnProcesar.TabIndex = 68
        Me.btnProcesar.Tag = "sub_procesar"
        Me.btnProcesar.Text = "Procesar"
        Me.btnProcesar.UseVisualStyleBackColor = True
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(1647, 81)
        Me.btnBuscar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(143, 28)
        Me.btnBuscar.TabIndex = 69
        Me.btnBuscar.Tag = "sub_accionBusqueda"
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(906, 606)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(4)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(888, 28)
        Me.ProgressBar1.TabIndex = 70
        Me.ProgressBar1.Tag = "progresoPlanilla"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(810, 613)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 17)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "Progreso"
        '
        'btnCancelPla
        '
        Me.btnCancelPla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelPla.Location = New System.Drawing.Point(574, 601)
        Me.btnCancelPla.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancelPla.Name = "btnCancelPla"
        Me.btnCancelPla.Size = New System.Drawing.Size(156, 38)
        Me.btnCancelPla.TabIndex = 72
        Me.btnCancelPla.Tag = "sub_cancelarPlanilla"
        Me.btnCancelPla.Text = "Cancelar Planilla"
        Me.btnCancelPla.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(775, 65)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 17)
        Me.Label7.TabIndex = 74
        Me.Label7.Text = "Comentario"
        '
        'txtComent
        '
        Me.txtComent.Location = New System.Drawing.Point(779, 85)
        Me.txtComent.Margin = New System.Windows.Forms.Padding(4)
        Me.txtComent.Name = "txtComent"
        Me.txtComent.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtComent.Properties.Appearance.Options.UseFont = True
        Me.txtComent.Size = New System.Drawing.Size(832, 20)
        Me.txtComent.TabIndex = 75
        Me.txtComent.Tag = "comentario"
        '
        'btnExpPlaCerr
        '
        Me.btnExpPlaCerr.Location = New System.Drawing.Point(1797, 81)
        Me.btnExpPlaCerr.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExpPlaCerr.Name = "btnExpPlaCerr"
        Me.btnExpPlaCerr.Size = New System.Drawing.Size(143, 28)
        Me.btnExpPlaCerr.TabIndex = 76
        Me.btnExpPlaCerr.Tag = "sub_expPlaCerrada"
        Me.btnExpPlaCerr.Text = "Exportar Planilla"
        Me.btnExpPlaCerr.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(326, 601)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(139, 39)
        Me.Button1.TabIndex = 77
        Me.Button1.Tag = "sub_Reconciliar"
        Me.Button1.Text = "Reconciliar"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'frmProcesarPlanilla
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1810, 653)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnExpPlaCerr)
        Me.Controls.Add(Me.txtComent)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnCancelPla)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnProcesar)
        Me.Controls.Add(Me.ugcPlanilla)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cboEstado)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.DateEdit3)
        Me.Controls.Add(Me.DateEdit2)
        Me.Controls.Add(Me.DateEdit1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtNroPla)
        Me.Controls.Add(Me.Label3)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmProcesarPlanilla"
        Me.Tag = "entPlanilla"
        Me.Text = "Procesar Planilla"
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtNroPla, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.DateEdit1, 0)
        Me.Controls.SetChildIndex(Me.DateEdit2, 0)
        Me.Controls.SetChildIndex(Me.DateEdit3, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.cboEstado, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.ugcPlanilla, 0)
        Me.Controls.SetChildIndex(Me.btnProcesar, 0)
        Me.Controls.SetChildIndex(Me.btnBuscar, 0)
        Me.Controls.SetChildIndex(Me.ProgressBar1, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.btnCancelPla, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.txtComent, 0)
        Me.Controls.SetChildIndex(Me.btnExpPlaCerr, 0)
        Me.Controls.SetChildIndex(Me.Button1, 0)
        CType(Me.DateEdit3.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNroPla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboEstado As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DateEdit3 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateEdit2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNroPla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ugcPlanilla As Util.uct_gridConBusqueda
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancelPla As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtComent As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnExpPlaCerr As System.Windows.Forms.Button
    Friend WithEvents Button1 As Button
End Class
