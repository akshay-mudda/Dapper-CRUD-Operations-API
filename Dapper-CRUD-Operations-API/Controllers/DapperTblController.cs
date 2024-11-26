using Dapper;
using Dapper_CRUD_Operations_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace Dapper_CRUD_Operations_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperTblController : ControllerBase
    {
        private readonly string connectionString;
        public DapperTblController (IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        [HttpPost]
        public IActionResult Create(TblDTOModel tblDTO)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Dapper_tbl " +
                    "(Column1, Column2, Column3, Column4, Column5, CreateDate)" +
                    "OUTPUT INSERTED.* " +
                    "VALUES (@Column1, @Column2, @Column3, @Column4, @Column5, @CreateDate)";

                    var Dpr = new TblModel()
                    {
                        Column1 = tblDTO.Column1,
                        Column2 = tblDTO.Column2,
                        Column3 = tblDTO.Column3,
                        Column4 = tblDTO.Column4,
                        Column5 = tblDTO.Column5,
                        CreateDate = DateTime.Now,
                    };

                    var newDpr = connection.QuerySingleOrDefault<TblModel>(sql, Dpr);
                    if (newDpr != null)
                    {
                        return Ok(newDpr);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("we have an exception: \n" + ex.Message);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Read()
        {
            List<TblModel> Dpr = new List<TblModel>();

            try
            {
                using (var  connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Dapper_tbl";
                    var data = connection.Query<TblModel>(sql);
                    Dpr = data.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok(Dpr);
        }

        [HttpGet("{id}")]
        public IActionResult GetID(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Dapper_tbl Where id=@id";
                    var data = connection.QuerySingleOrDefault<TblModel>(sql, new {Id = id});
                    if (data != null)
                    {
                        return Ok(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TblDTOModel tblDTO)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE Dapper_tbl SET Column1= @Column1, Column2= @Column2, Column3= @Column3, Column4= @Column4, Column5= @Column5 Where id=@id";

                    var Dpr = new TblModel()
                    {
                        Id = id,
                        Column1 = tblDTO.Column1,
                        Column2 = tblDTO.Column2,
                        Column3 = tblDTO.Column3,
                        Column4 = tblDTO.Column4,
                        Column5 = tblDTO.Column5,
                    };

                    int count = connection.Execute(sql, Dpr);
                    if (count < 1)
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return GetID(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "Delete FROM Dapper_tbl Where id=@id";
                    int count = connection.Execute(sql, new { Id = id });
                    if (count < 1)
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok();
        }
    }
}
