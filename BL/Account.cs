using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public class Account
    {
        public static ML.Result Add(ML.Account account)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UtapiaOptimissaContext context = new DL.UtapiaOptimissaContext())  //Se realiza la conexion al modelo de DB
                {
                    var query = context.Database.ExecuteSqlRaw($"AccountAdd '{account.account}', { account.balance}, '{ account.owner}'");  

                    if (query > 0) //se valida que tenga campos que agregar
                    {
                        //se valida que es correcto
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No fue posible dar de alta la cuenta";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            //retorna el resultado
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UtapiaOptimissaContext context = new DL.UtapiaOptimissaContext())  //Se realiza la conexion al modelo de DB
                {

                    var query = context.Accounts.FromSqlRaw("AccountGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null) //se valida que tenga campos que agregar
                    {
                        foreach (var obj in query)
                        {
                            ML.Account account = new ML.Account();

                            //se realiza un boxing para guardar los datos

                            account.account = obj.Account1;
                            account.balance = obj.Balance.Value;
                            account.owner = obj.Owner;
                            account.createdAt = obj.CreatedAt.ToString();

                            result.Objects.Add(account);

                        }
                        //se valida que es correcto
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se han encontrado pasajeros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            //retorna el resultado
            return result;
        }
        public static ML.Result GetByOwner(string owner)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UtapiaOptimissaContext context = new DL.UtapiaOptimissaContext())  //Se realiza la conexion al modelo de DB
                {
                    var quer = context.Accounts.FromSqlRaw($"AccountGetByOwner {owner}").AsEnumerable().ToList();
                    result.Objects = new List<object>();

                    if (quer != null) //se valida que tenga campos que agregar
                    {
                        foreach (var obj in quer )
                        {
                            ML.Account account = new ML.Account();

                            //se realiza un boxing para guardar los datos

                            account.account = obj.Account1;
                            account.balance = obj.Balance.Value;
                            account.owner = obj.Owner;
                            account.createdAt = obj.CreatedAt.ToString();

                            result.Objects.Add(account);

                        }
                        //se valida que es correcto
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al realizar la consulta de las cuentas del usuario";
                    }
                    //se valida que es correcto
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            //retorna el resultado
            return result;
        }
        public static ML.Result Balance(string account)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UtapiaOptimissaContext context = new DL.UtapiaOptimissaContext())  //Se realiza la conexion al modelo de DB
                {
                    var quer = context.Accounts.FromSqlRaw($"Balance {account}").AsEnumerable().ToList();
                    result.Objects = new List<object>();

                    if (quer != null) //se valida que tenga campos que agregar
                    {
                        foreach (var obj in quer)
                        {
                            ML.Account balance = new ML.Account();

                            //se realiza un boxing para guardar los datos

                            balance.account = obj.Account1;
                            balance.balance = obj.Balance.Value;
                            balance.owner = obj.Owner;
                            balance.createdAt = obj.CreatedAt.ToString();

                            result.Objects.Add(balance);

                        }
                        //se valida que es correcto
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al realizar la consulta de las cuentas del usuario";
                    }
                    //se valida que es correcto
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            //retorna el resultado
            return result;
        }

    }
}