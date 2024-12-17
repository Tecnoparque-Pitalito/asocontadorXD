<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Usuarios
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Usuarios))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LbFechaHora = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtNumeroCedula = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtFechaExpedicion = New System.Windows.Forms.MaskedTextBox()
        Me.txtLugarExpedicion = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNombreUsuario = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButsaveUsuario = New System.Windows.Forms.Button()
        Me.ButeliminarUsuario = New System.Windows.Forms.Button()
        Me.ButmodUsuario = New System.Windows.Forms.Button()
        Me.ButbuscarUsuario = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtLugarNacimiento = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFechaNacimiento = New System.Windows.Forms.MaskedTextBox()
        Me.DataGridViewUsuarios = New System.Windows.Forms.DataGridView()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridViewUsuarios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(77, Byte), Integer), CType(CType(97, Byte), Integer))
        Me.Panel1.Controls.Add(Me.LbFechaHora)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1100, 69)
        Me.Panel1.TabIndex = 18
        '
        'LbFechaHora
        '
        Me.LbFechaHora.AutoSize = True
        Me.LbFechaHora.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbFechaHora.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.LbFechaHora.Location = New System.Drawing.Point(91, 35)
        Me.LbFechaHora.Name = "LbFechaHora"
        Me.LbFechaHora.Size = New System.Drawing.Size(315, 21)
        Me.LbFechaHora.TabIndex = 8
        Me.LbFechaHora.Text = "Actualice, busque, cree o elimine un usuario."
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(27, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(57, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(90, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Usuarios"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1100, 26)
        Me.Panel3.TabIndex = 17
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
        Me.Button3.Location = New System.Drawing.Point(1029, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(20, 20)
        Me.Button3.TabIndex = 2
        Me.Button3.UseVisualStyleBackColor = True
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
        Me.Button1.Location = New System.Drawing.Point(1068, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(20, 20)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtNumeroCedula
        '
        Me.txtNumeroCedula.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumeroCedula.Location = New System.Drawing.Point(115, 50)
        Me.txtNumeroCedula.Name = "txtNumeroCedula"
        Me.txtNumeroCedula.Size = New System.Drawing.Size(276, 29)
        Me.txtNumeroCedula.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 589)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1100, 26)
        Me.Panel4.TabIndex = 22
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtFechaExpedicion)
        Me.GroupBox1.Controls.Add(Me.txtLugarExpedicion)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtNumeroCedula)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(27, 101)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(420, 254)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cédula"
        '
        'txtFechaExpedicion
        '
        Me.txtFechaExpedicion.Location = New System.Drawing.Point(213, 127)
        Me.txtFechaExpedicion.Mask = "00/00/0000"
        Me.txtFechaExpedicion.Name = "txtFechaExpedicion"
        Me.txtFechaExpedicion.Size = New System.Drawing.Size(178, 29)
        Me.txtFechaExpedicion.TabIndex = 3
        Me.txtFechaExpedicion.ValidatingType = GetType(Date)
        '
        'txtLugarExpedicion
        '
        Me.txtLugarExpedicion.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLugarExpedicion.Location = New System.Drawing.Point(213, 85)
        Me.txtLugarExpedicion.Name = "txtLugarExpedicion"
        Me.txtLugarExpedicion.Size = New System.Drawing.Size(178, 29)
        Me.txtLugarExpedicion.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label8.Location = New System.Drawing.Point(13, 89)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(187, 25)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Lugar de Expedición:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(13, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(188, 25)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Fecha de Expedición:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label4.Location = New System.Drawing.Point(13, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 25)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Número"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label5.Location = New System.Drawing.Point(15, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 25)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Nombres:"
        '
        'txtNombreUsuario
        '
        Me.txtNombreUsuario.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreUsuario.Location = New System.Drawing.Point(127, 33)
        Me.txtNombreUsuario.Name = "txtNombreUsuario"
        Me.txtNombreUsuario.Size = New System.Drawing.Size(266, 29)
        Me.txtNombreUsuario.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label7.Location = New System.Drawing.Point(15, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 25)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Teléfono:"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Controls.Add(Me.ButsaveUsuario, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ButeliminarUsuario, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.ButmodUsuario, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.ButbuscarUsuario, 0, 2)
        Me.TableLayoutPanel2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(881, 110)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(198, 178)
        Me.TableLayoutPanel2.TabIndex = 222
        '
        'ButsaveUsuario
        '
        Me.ButsaveUsuario.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.ButsaveUsuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButsaveUsuario.FlatAppearance.BorderSize = 0
        Me.ButsaveUsuario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ButsaveUsuario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.ButsaveUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButsaveUsuario.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.ButsaveUsuario.Location = New System.Drawing.Point(3, 3)
        Me.ButsaveUsuario.Name = "ButsaveUsuario"
        Me.ButsaveUsuario.Size = New System.Drawing.Size(192, 38)
        Me.ButsaveUsuario.TabIndex = 9
        Me.ButsaveUsuario.Text = "Guardar"
        Me.ButsaveUsuario.UseVisualStyleBackColor = False
        '
        'ButeliminarUsuario
        '
        Me.ButeliminarUsuario.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.ButeliminarUsuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButeliminarUsuario.FlatAppearance.BorderSize = 0
        Me.ButeliminarUsuario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ButeliminarUsuario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.ButeliminarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButeliminarUsuario.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.ButeliminarUsuario.Location = New System.Drawing.Point(3, 135)
        Me.ButeliminarUsuario.Name = "ButeliminarUsuario"
        Me.ButeliminarUsuario.Size = New System.Drawing.Size(192, 40)
        Me.ButeliminarUsuario.TabIndex = 12
        Me.ButeliminarUsuario.Text = "Eliminar"
        Me.ButeliminarUsuario.UseVisualStyleBackColor = False
        '
        'ButmodUsuario
        '
        Me.ButmodUsuario.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.ButmodUsuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButmodUsuario.FlatAppearance.BorderSize = 0
        Me.ButmodUsuario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ButmodUsuario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.ButmodUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButmodUsuario.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.ButmodUsuario.Location = New System.Drawing.Point(3, 47)
        Me.ButmodUsuario.Name = "ButmodUsuario"
        Me.ButmodUsuario.Size = New System.Drawing.Size(192, 38)
        Me.ButmodUsuario.TabIndex = 10
        Me.ButmodUsuario.Text = "Modificar"
        Me.ButmodUsuario.UseVisualStyleBackColor = False
        '
        'ButbuscarUsuario
        '
        Me.ButbuscarUsuario.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.ButbuscarUsuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButbuscarUsuario.FlatAppearance.BorderSize = 0
        Me.ButbuscarUsuario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ButbuscarUsuario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.ButbuscarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButbuscarUsuario.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.ButbuscarUsuario.Location = New System.Drawing.Point(3, 91)
        Me.ButbuscarUsuario.Name = "ButbuscarUsuario"
        Me.ButbuscarUsuario.Size = New System.Drawing.Size(192, 38)
        Me.ButbuscarUsuario.TabIndex = 11
        Me.ButbuscarUsuario.Text = "Buscar"
        Me.ButbuscarUsuario.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label9.Location = New System.Drawing.Point(16, 183)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 25)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "Email:"
        '
        'txtLugarNacimiento
        '
        Me.txtLugarNacimiento.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLugarNacimiento.Location = New System.Drawing.Point(209, 144)
        Me.txtLugarNacimiento.Name = "txtLugarNacimiento"
        Me.txtLugarNacimiento.Size = New System.Drawing.Size(184, 29)
        Me.txtLugarNacimiento.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label11.Location = New System.Drawing.Point(15, 113)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(193, 25)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "Fecha de Nacimiento:"
        '
        'txtTelefono
        '
        Me.txtTelefono.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelefono.Location = New System.Drawing.Point(127, 68)
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(266, 29)
        Me.txtTelefono.TabIndex = 5
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtEmail)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtFechaNacimiento)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtNombreUsuario)
        Me.GroupBox2.Controls.Add(Me.txtTelefono)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtLugarNacimiento)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(453, 101)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(422, 254)
        Me.GroupBox2.TabIndex = 223
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos Usuario"
        '
        'txtEmail
        '
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(127, 179)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(266, 29)
        Me.txtEmail.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.Location = New System.Drawing.Point(16, 148)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(189, 25)
        Me.Label3.TabIndex = 226
        Me.Label3.Text = "Lugar de nacimiento:"
        '
        'txtFechaNacimiento
        '
        Me.txtFechaNacimiento.Location = New System.Drawing.Point(214, 109)
        Me.txtFechaNacimiento.Mask = "00/00/0000"
        Me.txtFechaNacimiento.Name = "txtFechaNacimiento"
        Me.txtFechaNacimiento.Size = New System.Drawing.Size(179, 29)
        Me.txtFechaNacimiento.TabIndex = 6
        Me.txtFechaNacimiento.ValidatingType = GetType(Date)
        '
        'DataGridViewUsuarios
        '
        Me.DataGridViewUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.DataGridViewUsuarios.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders
        Me.DataGridViewUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewUsuarios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewUsuarios.Location = New System.Drawing.Point(3, 25)
        Me.DataGridViewUsuarios.Name = "DataGridViewUsuarios"
        Me.DataGridViewUsuarios.Size = New System.Drawing.Size(1046, 185)
        Me.DataGridViewUsuarios.TabIndex = 225
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.DataGridViewUsuarios)
        Me.GroupBox4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(27, 361)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1052, 213)
        Me.GroupBox4.TabIndex = 226
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Visualizar usuarios"
        '
        'Usuarios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1100, 615)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Usuarios"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Usuarios"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DataGridViewUsuarios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button3 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LbFechaHora As Label
    Friend WithEvents txtNumeroCedula As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtNombreUsuario As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtLugarExpedicion As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents ButsaveUsuario As Button
    Friend WithEvents ButeliminarUsuario As Button
    Friend WithEvents ButmodUsuario As Button
    Friend WithEvents ButbuscarUsuario As Button
    Friend WithEvents txtLugarNacimiento As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtFechaExpedicion As MaskedTextBox
    Friend WithEvents txtFechaNacimiento As MaskedTextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents DataGridViewUsuarios As DataGridView
    Friend WithEvents GroupBox4 As GroupBox
End Class
