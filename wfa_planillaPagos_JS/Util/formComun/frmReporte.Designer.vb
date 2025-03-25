<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReporte
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
        Me.uctDatos = New Util.uct_gridConBusqueda()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(12, 469)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 56
        Me.Button1.Tag = "cancel"
        Me.Button1.Text = "Cancelar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'uctDatos
        '
        Me.uctDatos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uctDatos.BackColor = System.Drawing.Color.White
        Me.uctDatos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.uctDatos.buscarPor = ""
        Me.uctDatos.ColAlternaColor = ""
        Me.uctDatos.ColElm = Nothing
        Me.uctDatos.conFiltro = True
        Me.uctDatos.conMenu = False
        Me.uctDatos.DataSource = Nothing
        Me.uctDatos.Location = New System.Drawing.Point(12, 12)
        Me.uctDatos.Name = "uctDatos"
        Me.uctDatos.ObjDetalle = Nothing
        Me.uctDatos.PermitirOrden = True
        Me.uctDatos.Size = New System.Drawing.Size(748, 448)
        Me.uctDatos.TabIndex = 57
        Me.uctDatos.Tabla = Nothing
        Me.uctDatos.TablaId = Nothing
        Me.uctDatos.TablaId2 = Nothing
        Me.uctDatos.Tag = "datos"
        Me.uctDatos.valorBusq = ""
        '
        'frmReporte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 501)
        Me.Controls.Add(Me.uctDatos)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmReporte"
        Me.Text = "Reporte"
        Me.Controls.SetChildIndex(Me.Button1, 0)
        Me.Controls.SetChildIndex(Me.uctDatos, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents uctDatos As Util.uct_gridConBusqueda
End Class
