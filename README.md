# UTapiaOptimissa
ExamenTecnicoOptimissa

- Instalar Script de la base de datos
- Ejecutar solución a través de Visual Studio
Consumo de API REST
Para realizar el consumo de API´S, ejecutar el proyecto "SL2"
Seleccionar el servicio que se desea consumir

Account
Agregar cuenta 
/api/Account/Add

 
Consultar todas las cuentas
/api/Account/GetAll
Buscar por Cuenta de cliente 
/api/Account/GetByOwner
 
Buscar el balance actual por cuenta especificada 
/api/Account/Balance
 

 

Para Transacción

Hacer una transferencia de cuenta, restando a la cuenta origen y sumando a la cuenta receptora
/api/Transaction/Trans
 
Consultar todas las transferencia que ha realizado una cuenta
/api/Transaction/GetByAccount
 
Consultar todas las transferencias enviadas desde una cuenta 
/api/Transaction/FromAccount
 
Consultar todas las transferencias recibidas desde una cuenta
/api/Transaction/ToAccount
 
