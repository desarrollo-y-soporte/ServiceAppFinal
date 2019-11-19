Imports System.ServiceProcess
Imports System.ComponentModel
Imports System.Configuration.Install

Public Class Instalador

    Private serviceInstaller As ServiceInstaller
    Private processInstaller As ServiceProcessInstaller

    Public Sub New()
        MyBase.New()

        'El Diseñador de componentes requiere esta llamada.
        InitializeComponent()

        'Agregue el código de inicialización después de llamar a InitializeComponent
        processInstaller = New ServiceProcessInstaller()
        serviceInstaller = New ServiceInstaller()
        processInstaller.Account = ServiceAccount.LocalSystem
        serviceInstaller.StartType = ServiceStartMode.Manual
        serviceInstaller.DisplayName = "SyP - Serive App"
        serviceInstaller.ServiceName = "SyP - Serive App"
        serviceInstaller.Description = "Servicio provisto por SyP para la comunicacion entre dispositivos."
        Installers.Add(serviceInstaller)
        Installers.Add(processInstaller)
    End Sub

End Class
