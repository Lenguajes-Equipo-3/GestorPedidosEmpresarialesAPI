USE [GestorPedidosEmpresariales_Proyecto2]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre_compania] [nvarchar](255) NULL,
	[nombre_contacto] [nvarchar](255) NULL,
	[apellido_contacto] [nvarchar](255) NULL,
	[puesto_contacto] [nvarchar](255) NULL,
	[direccion] [nvarchar](255) NULL,
	[ciudad] [nvarchar](255) NULL,
	[provincia] [nvarchar](255) NULL,
	[codigo_postal] [nvarchar](255) NULL,
	[pais] [nvarchar](255) NULL,
	[telefono] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamento](
	[id_departamento] [int] IDENTITY(1,1) NOT NULL,
	[nombre_departamento] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_departamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleOrden]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleOrden](
	[id_orden] [int] NOT NULL,
	[id_variante] [int] NOT NULL,
	[cantidad] [int] NULL,
	[precio_linea] [decimal](18, 0) NULL,
	[eliminado] [bit] NULL,
	[id_detalle_orden] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_detalle_orden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[id_empleado] [int] IDENTITY(1,1) NOT NULL,
	[nombre_empleado] [nvarchar](255) NULL,
	[apellidos_empleado] [nvarchar](255) NULL,
	[puesto] [nvarchar](255) NULL,
	[extension] [nvarchar](255) NULL,
	[telefono_trabajo] [nvarchar](255) NULL,
	[id_departamento] [int] NULL,
	[id_rol] [int] NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_empleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MetodoPago]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetodoPago](
	[id_metodo_pago] [int] IDENTITY(1,1) NOT NULL,
	[metodo_pago] [nvarchar](255) NULL,
	[tarjeta_credito] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_metodo_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orden]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orden](
	[id_orden] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NULL,
	[id_empleado] [int] NULL,
	[fecha_orden] [datetime] NULL,
	[direccion_viaje] [nvarchar](255) NULL,
	[ciudad_viaje] [nvarchar](255) NULL,
	[provincia_viaje] [nvarchar](255) NULL,
	[pais_viaje] [nvarchar](255) NULL,
	[telefono_viaje] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_orden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pago]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pago](
	[id_pago] [int] IDENTITY(1,1) NOT NULL,
	[id_orden] [int] NULL,
	[cantidad_pago] [decimal](18, 0) NULL,
	[fecha_pago] [datetime] NULL,
	[num_tarjetaCredito] [nvarchar](255) NULL,
	[nom_usuario_tarjeta] [nvarchar](255) NULL,
	[id_metodoPago] [int] NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParametrosSistema]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParametrosSistema](
	[id_parametro] [int] IDENTITY(1,1) NOT NULL,
	[nombre_empresa] [nvarchar](100) NULL,
	[direccion] [nvarchar](255) NULL,
	[telefono] [nvarchar](50) NULL,
	[correo] [nvarchar](100) NULL,
	[impuesto_ventas] [decimal](5, 2) NULL,
	[logo] [varbinary](max) NULL,
	[moneda] [nvarchar](10) NULL,
	[ultima_actualizacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_parametro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoBase]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoBase](
	[id_producto_base] [int] IDENTITY(1,1) NOT NULL,
	[nombre_producto] [nvarchar](255) NULL,
	[id_categoria] [int] NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_producto_base] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[id_rol] [int] IDENTITY(1,1) NOT NULL,
	[nombre_rol] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[id_empleado] [int] NULL,
	[correo] [nchar](100) NULL,
	[contrasenna] [nchar](100) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VarianteProducto]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VarianteProducto](
	[id_variante] [int] IDENTITY(1,1) NOT NULL,
	[id_producto_base] [int] NULL,
	[talla] [nvarchar](255) NULL,
	[descripcion] [nvarchar](255) NULL,
	[precio] [decimal](18, 0) NULL,
	[cantidad_existencias] [decimal](18, 0) NULL,
	[punto_reorden] [int] NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_variante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categoria] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Cliente] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Departamento] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[DetalleOrden] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Empleado] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[MetodoPago] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Orden] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Pago] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[ParametrosSistema] ADD  DEFAULT (getdate()) FOR [ultima_actualizacion]
GO
ALTER TABLE [dbo].[ProductoBase] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Rol] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[VarianteProducto] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[DetalleOrden]  WITH CHECK ADD FOREIGN KEY([id_orden])
REFERENCES [dbo].[Orden] ([id_orden])
GO
ALTER TABLE [dbo].[DetalleOrden]  WITH CHECK ADD FOREIGN KEY([id_variante])
REFERENCES [dbo].[VarianteProducto] ([id_variante])
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD FOREIGN KEY([id_departamento])
REFERENCES [dbo].[Departamento] ([id_departamento])
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD FOREIGN KEY([id_rol])
REFERENCES [dbo].[Rol] ([id_rol])
GO
ALTER TABLE [dbo].[Orden]  WITH CHECK ADD FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Cliente] ([id_cliente])
GO
ALTER TABLE [dbo].[Orden]  WITH CHECK ADD FOREIGN KEY([id_empleado])
REFERENCES [dbo].[Empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([id_metodoPago])
REFERENCES [dbo].[MetodoPago] ([id_metodo_pago])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([id_orden])
REFERENCES [dbo].[Orden] ([id_orden])
GO
ALTER TABLE [dbo].[ProductoBase]  WITH CHECK ADD FOREIGN KEY([id_categoria])
REFERENCES [dbo].[Categoria] ([id_categoria])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Empleado] FOREIGN KEY([id_empleado])
REFERENCES [dbo].[Empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Empleado]
GO
ALTER TABLE [dbo].[VarianteProducto]  WITH CHECK ADD FOREIGN KEY([id_producto_base])
REFERENCES [dbo].[ProductoBase] ([id_producto_base])
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEmpleado]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[ActualizarEmpleado]
	@id_empleado INT,
	@nombre_empleado NVARCHAR(255),
	@apellidos_empleado NVARCHAR(255),
	@puesto NVARCHAR(255) = NULL,
	@extension NVARCHAR(255) = NULL,
	@telefono_trabajo NVARCHAR(255) = NULL,
	@id_departamento INT,
	@id_rol INT,
	@eliminado BIT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		IF NOT EXISTS(SELECT 1 FROM Departamento WHERE id_departamento = @id_departamento)
		BEGIN
			THROW 50001, 'Error: El ID de departamento ingresado no es válido o no existe.', 1;
		END

		IF NOT EXISTS(SELECT 1 FROM Rol WHERE id_rol = @id_rol)
		BEGIN
			THROW 50002, 'Error: El ID de rol ingresado no es válido o no existe.', 1;
		END

		UPDATE Empleado
		SET
			nombre_empleado = @nombre_empleado,
            apellidos_empleado = @apellidos_empleado,
            puesto = @puesto,
            extension = @extension,
            telefono_trabajo = @telefono_trabajo,
            id_departamento = @id_departamento,
            id_rol = @id_rol,
            eliminado = @eliminado
		WHERE 
			id_empleado = @id_empleado;

	END TRY
	BEGIN CATCH
		THROW;
	END CATCH

	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[BorrarEmpleado]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[BorrarEmpleado]
    @id_empleado INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE dbo.Empleados
        SET
            eliminado = 1
        WHERE
            id_empleado = @id_empleado;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH

    SET NOCOUNT OFF;
END

GO
/****** Object:  StoredProcedure [dbo].[InsertarEmpleado]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[InsertarEmpleado]
	@nombre_empleado NVARCHAR(255),
	@apellidos_empleado NVARCHAR(255),
	@puesto NVARCHAR(255) = NULL,
	@extension NVARCHAR(255) = NULL,
	@telefono_trabajo NVARCHAR(255) = NULL,
	@id_departamento INT,
	@id_rol INT,
	@eliminado BIT = 0 --Valor por defecto porque los nuevos empleados no estan eliminados
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION;
	BEGIN TRY

		IF NOT EXISTS (SELECT 1 FROM Departamento WHERE id_departamento = @id_departamento)
		BEGIN
			THROW 50001, 'Error: El ID de departamento ingresado no es válido o no existe.', 1;
		END

		IF NOT EXISTS (SELECT 1 FROM Rol WHERE id_rol = @id_rol)
		BEGIN
			THROW 50002, 'Error: El ID de rol ingresado no es válido o no existe.', 1;
		END

		INSERT INTO Empleado (
			nombre_empleado,
			apellidos_empleado,
			puesto,
			extension,
			telefono_trabajo,
			id_departamento,
			id_rol,
			eliminado
		)
		VALUES (
			@nombre_empleado,
			@apellidos_empleado,
			@puesto,
			@extension,
			@telefono_trabajo,
			@id_departamento,
			@id_rol,
			@eliminado
		);

		COMMIT TRANSACTION;

		SELECT SCOPE_IDENTITY() AS NuevoEmpleadoID;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerEmpleados]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ObtenerEmpleados]
	@IncluirEliminados BIT = 0
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		 e.id_empleado,
        e.nombre_empleado,
        e.apellidos_empleado,
        e.puesto,
        e.extension,
        e.telefono_trabajo,
        e.eliminado,
        d.id_departamento,
        d.nombre_departamento,
        r.id_rol,
        r.nombre_rol
	FROM
		Empleado AS e
	INNER JOIN
		Departamento AS d ON e.id_departamento = d.id_departamento
	INNER JOIN
		Rol AS r ON e.id_rol = r.id_rol
	WHERE
		e.eliminado = 0 OR @IncluirEliminados = 1
	ORDER BY
		e.apellidos_empleado, e.nombre_empleado;
	
	SET NOCOUNT OFF;
END

GO
/****** Object:  StoredProcedure [dbo].[ObtenerPorIdEmpleado]    Script Date: 7/1/2025 12:55:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ObtenerPorIdEmpleado]
	@id_empleado INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        e.id_empleado,
        e.nombre_empleado,
        e.apellidos_empleado,
        e.puesto,
        e.extension,
        e.telefono_trabajo,
        e.eliminado,
        d.id_departamento,
        d.nombre_departamento,
        r.id_rol,
        r.nombre_rol
    FROM
        dbo.Empleados AS e
    INNER JOIN
        dbo.Departamentos AS d ON e.id_departamento = d.id_departamento
    INNER JOIN
        dbo.Roles AS r ON e.id_rol = r.id_rol
    WHERE
        -- Busca por el ID específico y se asegura de que no esté eliminado
        e.id_empleado = @id_empleado AND e.eliminado = 0;

    SET NOCOUNT OFF;
END

GO

use GestorPedidosEmpresariales_Proyecto2
-- Primero eliminar las tablas dependientes (detalle), luego las maestras (padre)

--DROP TABLE [DetalleOrden];       -- Depende de Orden y VarianteProducto
--DROP TABLE  [Pago];              -- Depende de Orden y MetodoPago
--DROP TABLE  [Orden];             -- Depende de Cliente y Empleado
--DROP TABLE  [Usuario];           -- Depende de Empleado
--DROP TABLE  [VarianteProducto];  -- Depende de ProductoBase
--DROP TABLE  [ProductoBase];      -- Depende de Categoria

---- Tablas que no dependen de otras (o tienen menos dependencias)
--DROP TABLE  [Empleado];          -- Depende de Departamento y Rol
--DROP TABLE  [Cliente];
--DROP TABLE [Categoria];
--DROP TABLE [MetodoPago];
--DROP TABLE  [Departamento];
--DROP TABLE  [Rol];
--DROP TABLE  [ParametrosSistema];
