Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class SettingsResetDlg

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub SettingsResetDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BackColor = CurrentTheme.SectionBackgroundColor
        ForeColor = CurrentTheme.ForegroundColor
        Select Case MainForm.Language
            Case 0
                Select Case My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName
                    Case "ENU", "ENG"
                        Text = "Reset preferences"
                        Label1.Text = "If you proceed, the settings will be reset to their default values. Once this process is complete, you'll return to the main program window." & CrLf & CrLf & "Do you want to proceed?"
                        OK_Button.Text = "Yes"
                        Cancel_Button.Text = "No"
                    Case "ESN"
                        Text = "Restablecer preferencias"
                        Label1.Text = "Si continúa, las configuraciones serán restablecidas a sus valores predeterminados. Cuando este proceso haya completado, regresará a la ventana principal." & CrLf & CrLf & "¿Desea continuar?"
                        OK_Button.Text = "Sí"
                        Cancel_Button.Text = "No"
                    Case "FRA"
                        Text = "Réinitialiser les préférences"
                        Label1.Text = "Si vous continuez, les paramètres seront réinitialisés à leurs valeurs par défaut. Une fois ce processus terminé, vous reviendrez à la fenêtre principale du programme." & CrLf & CrLf & "Voulez-vous continuer ?"
                        OK_Button.Text = "Oui"
                        Cancel_Button.Text = "Non"
                    Case "PTB", "PTG"
                        Text = "Repor preferências"
                        Label1.Text = "Se prosseguir, as configurações serão repostas para os valores predefinidos. Quando este processo estiver concluído, regressará à janela principal do programa." & CrLf & CrLf & "Deseja continuar?"
                        OK_Button.Text = "Sim"
                        Cancel_Button.Text = "Não"
                    Case "ITA"
                        Text = "Ripristina preferenze"
                        Label1.Text = "Se procedi, le impostazioni verranno ripristinate ai valori predefiniti. Al termine di questo processo, si tornerà alla finestra principale del programma." & CrLf & CrLf & "Vuoi procedere?"
                        OK_Button.Text = "Sì"
                        Cancel_Button.Text = "No"
                    Case "CHS", "CHT"
                        Text = "重置首选项"
                        Label1.Text = "如果继续，设置将重置为默认值。此过程完成后，您将返回程序主窗口。" & CrLf & CrLf & "是否要继续？"
                        OK_Button.Text = "是"
                        Cancel_Button.Text = "否"
                End Select
            Case 1
                Text = "Reset preferences"
                Label1.Text = "If you proceed, the settings will be reset to their default values. Once this process is complete, you'll return to the main program window." & CrLf & CrLf & "Do you want to proceed?"
                OK_Button.Text = "Yes"
                Cancel_Button.Text = "No"
            Case 2
                Text = "Restablecer preferencias"
                Label1.Text = "Si continúa, las configuraciones serán restablecidas a sus valores predeterminados. Cuando este proceso haya completado, regresará a la ventana principal." & CrLf & CrLf & "¿Desea continuar?"
                OK_Button.Text = "Sí"
                Cancel_Button.Text = "No"
            Case 3
                Text = "Réinitialiser les préférences"
                Label1.Text = "Si vous continuez, les paramètres seront réinitialisés à leurs valeurs par défaut. Une fois ce processus terminé, vous reviendrez à la fenêtre principale du programme." & CrLf & CrLf & "Voulez-vous continuer ?"
                OK_Button.Text = "Oui"
                Cancel_Button.Text = "Non"
            Case 4
                Text = "Repor preferências"
                Label1.Text = "Se prosseguir, as configurações serão repostas para os valores predefinidos. Quando este processo estiver concluído, regressará à janela principal do programa." & CrLf & CrLf & "Deseja continuar?"
                OK_Button.Text = "Sim"
                Cancel_Button.Text = "Não"
            Case 5
                Text = "Ripristino preferenze"
                Label1.Text = "Se procedi, le impostazioni verranno ripristinate ai valori predefiniti. Al termine di questo processo, si tornerà alla finestra principale del programma." & CrLf & CrLf & "Vuoi procedere?"
                OK_Button.Text = "Sì"
                Cancel_Button.Text = "No"
            Case 6
                Text = "重置首选项"
                Label1.Text = "如果继续，设置将重置为默认值。此过程完成后，您将返回程序主窗口。" & CrLf & CrLf & "是否要继续？"
                OK_Button.Text = "是"
                Cancel_Button.Text = "否"

        End Select
        Dim handle As IntPtr = WindowHelper.GetWindowHandle(Me)
        WindowHelper.ToggleDarkTitleBar(handle, CurrentTheme.IsDark)
        ThemeHelper.UpdateLinkLabelColors(Me, Color.DodgerBlue, CurrentTheme.AccentColors(0))
        Beep()
    End Sub
End Class
