<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Principal
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Principal))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LB_Rol = New System.Windows.Forms.Label()
        Me.LB_User = New System.Windows.Forms.Label()
        Me.LbFechaHora = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguraciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiasDeSeguridadYRespaldoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsuariosDelSistemaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GestionDePeriodosYCostosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarFacturaciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsuariosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CrearUsuarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PuntosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GestiónDePrediosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IrAReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.AsignarCostosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 749)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1552, 0)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.Panel1.Controls.Add(Me.LB_Rol)
        Me.Panel1.Controls.Add(Me.LB_User)
        Me.Panel1.Controls.Add(Me.LbFechaHora)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1552, 153)
        Me.Panel1.TabIndex = 4
        '
        'LB_Rol
        '
        Me.LB_Rol.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_Rol.AutoSize = True
        Me.LB_Rol.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_Rol.ForeColor = System.Drawing.Color.DarkRed
        Me.LB_Rol.Location = New System.Drawing.Point(1323, 126)
        Me.LB_Rol.Name = "LB_Rol"
        Me.LB_Rol.Size = New System.Drawing.Size(28, 21)
        Me.LB_Rol.TabIndex = 9
        Me.LB_Rol.Text = "---"
        '
        'LB_User
        '
        Me.LB_User.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_User.AutoSize = True
        Me.LB_User.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_User.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.LB_User.Location = New System.Drawing.Point(1323, 105)
        Me.LB_User.Name = "LB_User"
        Me.LB_User.Size = New System.Drawing.Size(28, 21)
        Me.LB_User.TabIndex = 8
        Me.LB_User.Text = "---"
        '
        'LbFechaHora
        '
        Me.LbFechaHora.AutoSize = True
        Me.LbFechaHora.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbFechaHora.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.LbFechaHora.Location = New System.Drawing.Point(87, 102)
        Me.LbFechaHora.Name = "LbFechaHora"
        Me.LbFechaHora.Size = New System.Drawing.Size(36, 25)
        Me.LbFechaHora.TabIndex = 7
        Me.LbFechaHora.Text = "---"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.Button2)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1552, 26)
        Me.Panel3.TabIndex = 6
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), System.Drawing.Image)
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(1445, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(20, 20)
        Me.Button3.TabIndex = 2
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(1483, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(20, 20)
        Me.Button2.TabIndex = 1
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(1520, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(20, 20)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label2.Location = New System.Drawing.Point(87, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(189, 25)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "P2024-112805-14474"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(86, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1221, 25)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Software de gestión integral de recaudo y facturación para los usuarios del distr" &
    "ito de adecuación de tierras de pequeña escala contador"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(12, 43)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(68, 84)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1327, 53)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(176, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalirToolStripMenuItem})
        Me.ArchivoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ArchivoToolStripMenuItem.Image = CType(resources.GetObject("ArchivoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(79, 34)
        Me.ArchivoToolStripMenuItem.Text = "Archivo"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(101, 22)
        Me.SalirToolStripMenuItem.Text = "Salir"
        '
        'ConfiguraciónToolStripMenuItem
        '
        Me.ConfiguraciónToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopiasDeSeguridadYRespaldoToolStripMenuItem, Me.UsuariosDelSistemaToolStripMenuItem})
        Me.ConfiguraciónToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ConfiguraciónToolStripMenuItem.Image = CType(resources.GetObject("ConfiguraciónToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ConfiguraciónToolStripMenuItem.Name = "ConfiguraciónToolStripMenuItem"
        Me.ConfiguraciónToolStripMenuItem.Size = New System.Drawing.Size(117, 34)
        Me.ConfiguraciónToolStripMenuItem.Text = "Configuración"
        '
        'CopiasDeSeguridadYRespaldoToolStripMenuItem
        '
        Me.CopiasDeSeguridadYRespaldoToolStripMenuItem.Name = "CopiasDeSeguridadYRespaldoToolStripMenuItem"
        Me.CopiasDeSeguridadYRespaldoToolStripMenuItem.Size = New System.Drawing.Size(264, 22)
        Me.CopiasDeSeguridadYRespaldoToolStripMenuItem.Text = "Copias de seguridad y respaldo"
        '
        'UsuariosDelSistemaToolStripMenuItem
        '
        Me.UsuariosDelSistemaToolStripMenuItem.Name = "UsuariosDelSistemaToolStripMenuItem"
        Me.UsuariosDelSistemaToolStripMenuItem.Size = New System.Drawing.Size(264, 22)
        Me.UsuariosDelSistemaToolStripMenuItem.Text = "Usuarios del Sistema"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.UsuariosToolStripMenuItem, Me.PuntosToolStripMenuItem, Me.ReportesToolStripMenuItem, Me.ConfiguraciónToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1552, 38)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GestionDePeriodosYCostosToolStripMenuItem})
        Me.ToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripMenuItem1.Image = CType(resources.GetObject("ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(88, 34)
        Me.ToolStripMenuItem1.Text = "Periodos"
        '
        'GestionDePeriodosYCostosToolStripMenuItem
        '
        Me.GestionDePeriodosYCostosToolStripMenuItem.Name = "GestionDePeriodosYCostosToolStripMenuItem"
        Me.GestionDePeriodosYCostosToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.GestionDePeriodosYCostosToolStripMenuItem.Text = "Periodos y Costos Anuales"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerarFacturaciónToolStripMenuItem, Me.GenerarToolStripMenuItem, Me.AsignarCostosToolStripMenuItem})
        Me.ToolStripMenuItem2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripMenuItem2.Image = CType(resources.GetObject("ToolStripMenuItem2.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(151, 34)
        Me.ToolStripMenuItem2.Text = "Facturación / Pagos"
        '
        'GenerarFacturaciónToolStripMenuItem
        '
        Me.GenerarFacturaciónToolStripMenuItem.Name = "GenerarFacturaciónToolStripMenuItem"
        Me.GenerarFacturaciónToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.GenerarFacturaciónToolStripMenuItem.Text = "Generar Pagos"
        '
        'GenerarToolStripMenuItem
        '
        Me.GenerarToolStripMenuItem.Name = "GenerarToolStripMenuItem"
        Me.GenerarToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.GenerarToolStripMenuItem.Text = "Generar  Facturas"
        '
        'UsuariosToolStripMenuItem
        '
        Me.UsuariosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CrearUsuarioToolStripMenuItem})
        Me.UsuariosToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.UsuariosToolStripMenuItem.Image = CType(resources.GetObject("UsuariosToolStripMenuItem.Image"), System.Drawing.Image)
        Me.UsuariosToolStripMenuItem.Name = "UsuariosToolStripMenuItem"
        Me.UsuariosToolStripMenuItem.Size = New System.Drawing.Size(87, 34)
        Me.UsuariosToolStripMenuItem.Text = "Usuarios"
        '
        'CrearUsuarioToolStripMenuItem
        '
        Me.CrearUsuarioToolStripMenuItem.Name = "CrearUsuarioToolStripMenuItem"
        Me.CrearUsuarioToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.CrearUsuarioToolStripMenuItem.Text = "Gestión de Usuarios"
        '
        'PuntosToolStripMenuItem
        '
        Me.PuntosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GestiónDePrediosToolStripMenuItem})
        Me.PuntosToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.PuntosToolStripMenuItem.Image = CType(resources.GetObject("PuntosToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PuntosToolStripMenuItem.Name = "PuntosToolStripMenuItem"
        Me.PuntosToolStripMenuItem.Size = New System.Drawing.Size(80, 34)
        Me.PuntosToolStripMenuItem.Text = "Predios"
        '
        'GestiónDePrediosToolStripMenuItem
        '
        Me.GestiónDePrediosToolStripMenuItem.Name = "GestiónDePrediosToolStripMenuItem"
        Me.GestiónDePrediosToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.GestiónDePrediosToolStripMenuItem.Text = "Gestión de predios"
        '
        'ReportesToolStripMenuItem
        '
        Me.ReportesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IrAReportesToolStripMenuItem})
        Me.ReportesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ReportesToolStripMenuItem.Image = CType(resources.GetObject("ReportesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
        Me.ReportesToolStripMenuItem.Size = New System.Drawing.Size(89, 34)
        Me.ReportesToolStripMenuItem.Text = "Reportes"
        '
        'IrAReportesToolStripMenuItem
        '
        Me.IrAReportesToolStripMenuItem.Name = "IrAReportesToolStripMenuItem"
        Me.IrAReportesToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.IrAReportesToolStripMenuItem.Text = "Ir a reportes"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.MenuStrip1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 153)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1552, 38)
        Me.Panel2.TabIndex = 5
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 723)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1552, 26)
        Me.Panel4.TabIndex = 7
        '
        'AsignarCostosToolStripMenuItem
        '
        Me.AsignarCostosToolStripMenuItem.Name = "AsignarCostosToolStripMenuItem"
        Me.AsignarCostosToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.AsignarCostosToolStripMenuItem.Text = "Asignar Costos"
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1552, 749)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Principal"
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "..:: Software de gestion integral de recaudo y facturacion ASOCONTADOR ::.."
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ArchivoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConfiguraciónToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents LbFechaHora As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents UsuariosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CrearUsuarioToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PuntosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LB_User As Label
    Friend WithEvents LB_Rol As Label
    Friend WithEvents CopiasDeSeguridadYRespaldoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GestiónDePrediosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents GestionDePeriodosYCostosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents GenerarFacturaciónToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UsuariosDelSistemaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IrAReportesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GenerarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AsignarCostosToolStripMenuItem As ToolStripMenuItem
End Class
