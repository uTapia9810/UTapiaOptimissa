using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Transaction
    {
        public static ML.Result Trans (ML.Transaction transaction)
        {
            ML.Result result = new ML.Result();
            try
            {
                using ( DL.UtapiaOptimissaContext context = new DL.UtapiaOptimissaContext())   //Se realiza la conexion al modelo de DB
                {
                    var query = context.Database.ExecuteSqlRaw($"Trans '{transaction.FromAccount}', '{transaction.ToAccount}', { transaction.Amount}");

                    if (query > 1) //se valida que tenga campos que agregar
                    {
                        //se valida que es correcto
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Transferencia no realizada";
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
        public static ML.Result GetAccount(string account)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UtapiaOptimissaContext context = new DL.UtapiaOptimissaContext())   //Se realiza la conexion al modelo de DB
                {

                    var query = context.Transacctions.FromSqlRaw($"AccountGetByAccount '{account}'").AsEnumerable();
                    result.Objects = new List<object>();

                    if (query != null)  //se valida que tenga campos que agregar
                    {
                        foreach (var obj in query)
                        {
                            ML.Transaction transaction = new ML.Transaction();

                            //se realiza un boxing para guardar los datos
                            transaction.IdTransaction = obj.Idtransaction;
                            transaction.FromAccount = obj.Fromaccount;
                            transaction.ToAccount = obj.Toaccount;
                            transaction.Amount = obj.Amount.Value;
                            transaction.SentAt = obj.SentAt.Value.ToString("dd-MM-yyyy HH:mm:ss.fffff");

                            result.Objects.Add(transaction);

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
        public static ML.Result FromAccount(string fromaccount)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UtapiaOptimissaContext context = new DL.UtapiaOptimissaContext())  //Se realiza la conexion al modelo de DB
                {
                    var quer = context.Transacctions.FromSqlRaw($"TransFromAccount {fromaccount}").AsEnumerable();
                    result.Objects = new List<object>();

                    if (quer != null)  //se valida que tenga campos que agregar
                    {
                        foreach (var obj in quer)
                        {
                            ML.Transaction transaction = new ML.Transaction();

                            //se realiza un boxing para guardar los datos
                            transaction.IdTransaction = obj.Idtransaction;
                            transaction.FromAccount = obj.Fromaccount;
                            transaction.ToAccount = obj.Toaccount;
                            transaction.Amount = obj.Amount.Value;
                            transaction.SentAt = obj.SentAt.Value.ToString("dd-MM-yyyy HH:mm:ss.fffff");

                            result.Objects.Add(transaction);

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
        public static ML.Result ToAccount(string toaccount)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UtapiaOptimissaContext context = new DL.UtapiaOptimissaContext())   //Se realiza la conexion al modelo de DB
                {
                    var quer = context.Transacctions.FromSqlRaw($"TransToAccount {toaccount}").AsEnumerable().ToList().AsEnumerable();
                    result.Objects = new List<object>();

                    if (quer != null)   //se valida que tenga campos que agregar
                    {
                        foreach (var obj in quer)
                        {
                            ML.Transaction transaction = new ML.Transaction();

                            //se realiza un boxing para guardar los datos
                            transaction.IdTransaction = obj.Idtransaction;
                            transaction.FromAccount = obj.Fromaccount;
                            transaction.ToAccount = obj.Toaccount;
                            transaction.Amount = obj.Amount.Value;
                            transaction.SentAt = obj.SentAt.Value.ToString("dd-MM-yyyy HH:mm:ss.fffff");

                            result.Objects.Add(transaction);

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
