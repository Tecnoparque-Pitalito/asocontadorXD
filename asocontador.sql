CREATE TABLE administrador (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  Usuario TEXT NOT NULL,
  Contrasena TEXT NOT NULL,
  Rol TEXT NOT NULL
);

CREATE TABLE clasificacionuso (
  idClasificacionUso INTEGER PRIMARY KEY,
  TipoDeUso TEXT
);

CREATE TABLE costosfijos (
  idCostos INTEGER PRIMARY KEY,
  valor TEXT NOT NULL,
  ClasificacionUso_idClasificacionUso INTEGER NOT NULL,
  Periodos_idPeriodos INTEGER NOT NULL,
  FOREIGN KEY (ClasificacionUso_idClasificacionUso) REFERENCES clasificacionuso(idClasificacionUso),
  FOREIGN KEY (Periodos_idPeriodos) REFERENCES periodos(idPeriodos)
);

CREATE TABLE factura (
  idFactura INTEGER PRIMARY KEY,
  Predios_idPredios INTEGER NOT NULL,
  Periodos_idPeriodos INTEGER NOT NULL,
  FechaPago TEXT NULL,
  estado INTEGER DEFAULT 3,
  mes INTEGER,
  Observaciones TEXT NULL,
  addCostosAsociados TEXT NULL,
  FOREIGN KEY (Predios_idPredios) REFERENCES predios(idPredios),
  FOREIGN KEY (Periodos_idPeriodos) REFERENCES periodos(idPeriodos)
);

CREATE TABLE otroscostos (
  idOtrosCostos INTEGER PRIMARY KEY,
  CostoAsociado TEXT
);

CREATE TABLE "periodos" (
  idPeriodos INTEGER PRIMARY KEY,
  InicioPeriodo TEXT,
  FinPeriodo TEXT,
  FechaCreacion TEXT
);

CREATE TABLE predios (
  idPredios INTEGER PRIMARY KEY,
  Vereda TEXT,
  ServicioActivo INTEGER,
  AreasContruidas TEXT,
  AreasPorConstruir TEXT,
  DiametroTuveria TEXT,
  NombrePredio TEXT,
  ExtencionTotal TEXT,
  Observaciones TEXT,
  NumeroMatricula TEXT,
  NumeroRegistroCatastral TEXT,
  Usuarios_NumeroCedula INTEGER NOT NULL,
  ClasificacionUso_idClasificacionUso INTEGER,
  FOREIGN KEY (Usuarios_NumeroCedula) REFERENCES usuarios(NumeroCedula),
  FOREIGN KEY (ClasificacionUso_idClasificacionUso) REFERENCES clasificacionuso(idClasificacionUso)
);

CREATE TABLE usuarios (
  NumeroCedula INTEGER PRIMARY KEY UNIQUE,
  Nombre TEXT NOT NULL,
  LugarExpedicionCedula TEXT NOT NULL,
  FechaEpedicion TEXT,
  telefono TEXT,
  FechaNacimiento TEXT,
  LugarNacimiento TEXT,
  email TEXT
);

CREATE TABLE valorotroscostos (
  idValorOtrosCostos INTEGER PRIMARY KEY,
  Valor TEXT,
  OtrosCostos_idOtrosCostos INTEGER NOT NULL,
  Periodos_idPeriodos INTEGER NOT NULL,
  FOREIGN KEY (OtrosCostos_idOtrosCostos) REFERENCES otroscostos(idOtrosCostos),
  FOREIGN KEY (Periodos_idPeriodos) REFERENCES periodos(idPeriodos)
);
