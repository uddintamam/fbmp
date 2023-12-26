Imports System.Windows.Forms

Public Class ProgressBarColumn
    Inherits DataGridViewColumn

    Public Sub New()
        MyBase.New(New ProgressBarCell())
    End Sub

    Public Overrides Property CellTemplate As DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(value As DataGridViewCell)
            ' Pastikan sel template adalah ProgressBarCell
            If Not (TypeOf value Is ProgressBarCell) Then
                Throw New InvalidCastException("Cell template must be a ProgressBarCell")
            End If
            MyBase.CellTemplate = value
        End Set
    End Property
End Class
