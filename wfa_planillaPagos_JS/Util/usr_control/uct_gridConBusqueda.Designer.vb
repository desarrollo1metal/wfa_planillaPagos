<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uct_gridConBusqueda
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.gctDatos = New DevExpress.XtraGrid.GridControl()
        Me.gvwDatos = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtBuscar = New DevExpress.XtraEditors.TextEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.gctDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvwDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gctDatos
        '
        Me.gctDatos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gctDatos.Location = New System.Drawing.Point(3, 29)
        Me.gctDatos.MainView = Me.gvwDatos
        Me.gctDatos.Name = "gctDatos"
        Me.gctDatos.Size = New System.Drawing.Size(620, 416)
        Me.gctDatos.TabIndex = 0
        Me.gctDatos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvwDatos})
        '
        'gvwDatos
        '
        Me.gvwDatos.GridControl = Me.gctDatos
        Me.gvwDatos.Name = "gvwDatos"
        Me.gvwDatos.OptionsView.ColumnAutoWidth = False
        Me.gvwDatos.OptionsView.ShowGroupPanel = False
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(85, 5)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.txtBuscar.Properties.Appearance.Options.UseFont = True
        Me.txtBuscar.Size = New System.Drawing.Size(256, 18)
        Me.txtBuscar.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Buscar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(423, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Label2"
        Me.Label2.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(378, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Label3"
        Me.Label3.Visible = False
        '
        'uct_gridConBusqueda
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBuscar)
        Me.Controls.Add(Me.gctDatos)
        Me.Name = "uct_gridConBusqueda"
        Me.Size = New System.Drawing.Size(626, 448)
        CType(Me.gctDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvwDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gctDatos As DevExpress.XtraGrid.GridControl
    Friend WithEvents txtBuscar As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents gvwDatos As DevExpress.XtraGrid.Views.Grid.GridView

End Class
