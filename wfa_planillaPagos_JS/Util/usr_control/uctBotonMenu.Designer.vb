<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uctBotonMenu
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
        Me.lblTextoBtn = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblTextoBtn
        '
        Me.lblTextoBtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTextoBtn.AutoSize = True
        Me.lblTextoBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTextoBtn.Location = New System.Drawing.Point(10, 7)
        Me.lblTextoBtn.Name = "lblTextoBtn"
        Me.lblTextoBtn.Size = New System.Drawing.Size(40, 13)
        Me.lblTextoBtn.TabIndex = 0
        Me.lblTextoBtn.Text = "Boton"
        '
        'uctBotonMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblTextoBtn)
        Me.Name = "uctBotonMenu"
        Me.Size = New System.Drawing.Size(198, 28)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTextoBtn As System.Windows.Forms.Label

End Class
