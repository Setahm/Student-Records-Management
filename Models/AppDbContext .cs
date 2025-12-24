using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WebApplication1.Models
{
    public class AppDbContext
    {
        private readonly string _connStr;

        public AppDbContext()
        {
            _connStr = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
        }

        public List<Student> GetStudents()
        {
            var students = new List<Student>();
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM student;", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
{
    Id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id"),
    Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString("name"),
    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email")
});
                }
            }
            return students;
        }

        public Student GetStudentById(int id)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM student WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Student
{
    Id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id"),
    Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString("name"),
    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email")
                    };
                }
            }
            return null;
        }

        public void InsertStudent(Student student)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO student (name, email) VALUES (@name, @email);", conn);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE student SET name = @name, email = @email WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", student.Id);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int id)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM student WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}