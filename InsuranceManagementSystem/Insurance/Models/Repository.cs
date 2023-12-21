using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
#nullable disable

namespace Insurance.Models;

public class Repository{
    
    private static string connectionString;


 public static void Update(Add add)
{
            connectionString= getConnection();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand($"update Userdetails set Name='{add.Name}',Phoneno='{add.PhoneNumber}',Age={add.Age},City='{add.city}',Role='{add.Role}',Gender='{add.Gender}' where Userid={add.Userid}",connection);
            sqlcommand.ExecuteNonQuery();
            connection.Close();
            return;
}

 public  void AddEmployee(Add add)
{
            connectionString= getConnection();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand($"insert into Userdetails values({add.Userid},'{add.Name}','{add.Password}',{add.Age},'{add.Gender}','{add.Role}','{add.PhoneNumber}','{add.city}')",connection);
            sqlcommand.ExecuteNonQuery();
            connection.Close();
            return;
}

public static Add SeacrhEmployee(int Userid)
        {
            Add createEmpAccount = new Add();
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                SqlDataAdapter sqlDataAdapter=new SqlDataAdapter($"select * from Userdetails where Userid='{Userid}'",connection);
                DataTable dataTable=new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    
                    createEmpAccount.Name=dataRow["Name"].ToString();
                    createEmpAccount.Userid=Convert.ToInt32(dataRow["Userid"].ToString());
                    createEmpAccount.PhoneNumber=(dataRow["Phoneno"].ToString());
                    createEmpAccount.Age=Convert.ToInt32(dataRow["Age"].ToString());
                    createEmpAccount.Role=dataRow["Role"].ToString();
                    createEmpAccount.city=dataRow["City"].ToString();
                    createEmpAccount.Gender=dataRow["Gender"].ToString();
                    
                }
                }
                
            }
            catch (SqlException e)
            {                  
                Console.WriteLine("Datebase error"+e);               
            }
            return createEmpAccount;
        }
public static List<Add> ViewEmployee()
        {
            List<Add> EmployeeList = new List<Add>();
            
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                SqlDataAdapter sqlDataAdapter=new SqlDataAdapter("select * from Userdetails where Role='Employee'",connection);
                DataTable dataTable=new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Add createEmpAccount = new Add();
                    createEmpAccount.Name=dataRow["Name"].ToString();
                    createEmpAccount.Userid=Convert.ToInt32(dataRow["Userid"]);
                    createEmpAccount.PhoneNumber=(dataRow["Phoneno"].ToString());
                    createEmpAccount.Age=Convert.ToInt32(dataRow["Age"]);
                    createEmpAccount.Role=dataRow["Role"].ToString();
                    createEmpAccount.city=dataRow["City"].ToString();
                    createEmpAccount.Gender=dataRow["Gender"].ToString();
                    EmployeeList.Add(createEmpAccount);
                    
                }
                }
                
            }
            catch (SqlException e)
            {                  
                Console.WriteLine("Datebase error"+e);               
            }
            return EmployeeList;
        }

        

public static List<Policy> ApplingPolicy(int userid)
        {
            List<Policy> policylist = new List<Policy>();
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                SqlDataAdapter sqlDataAdapter=new SqlDataAdapter($"select * from Policy where Userid={userid}",connection);
                DataTable dataTable=new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Policy policy = new Policy();
                    policy.PolicyID=Convert.ToInt32(dataRow["SNo"]);
                   policy.Name=dataRow["Name"].ToString();
                    policy.Userid=Convert.ToInt32(dataRow["Userid"]);
                    policy.Type=dataRow["Policytype"].ToString();
                    policy.Age=Convert.ToInt32(dataRow["Age"]);
                    policy.ClaimAmount=(dataRow["Claimamount"].ToString());
                    policy.PolicyDuration=Convert.ToString(dataRow["Policyduration"]);
                    policy.PolicyDate=Convert.ToDateTime(dataRow["Policydate"]);
                    
                   
                    policylist.Add(policy);
                    
                }
                }
                
            }
            catch (SqlException e)
            {                  
                Console.WriteLine("Database error"+e);               
            }
            return policylist;
        }
public static List<Policy> ApplingPolicy()
        {
            List<Policy> policylist = new List<Policy>();
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                SqlDataAdapter sqlDataAdapter=new SqlDataAdapter($"select * from Policy",connection);
                DataTable dataTable=new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Policy policy = new Policy();
                    policy.PolicyID=Convert.ToInt32(dataRow["SNo"]);
                   policy.Name=dataRow["Name"].ToString();
                    policy.Userid=Convert.ToInt32(dataRow["Userid"]);
                    policy.Type=dataRow["Policytype"].ToString();
                    policy.Age=Convert.ToInt32(dataRow["Age"].ToString());
                    policy.ClaimAmount=(dataRow["Claimamount"].ToString());
                    policy.PolicyDuration=Convert.ToString(dataRow["Policyduration"]);
                    policy.PolicyDate=Convert.ToDateTime(dataRow["Policydate"]);
                    
                   
                    policylist.Add(policy);
                    
                }
                }
                
            }
            catch (SqlException e)
            {                  
                Console.WriteLine("Database error"+e);               
            }
            return policylist;
        }
public  void AddPolicy(Policy policy)
{
            connectionString= getConnection();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand($"insert into Policy values('{policy.Name}',{policy.Userid},{policy.Age},'{policy.Type}','{policy.ClaimAmount}','{policy.PolicyDuration}',@date)",connection);
            sqlcommand.Parameters.AddWithValue("@date",policy.PolicyDate);
            sqlcommand.ExecuteNonQuery();
            connection.Close();
            return;
}
    public string login(SignUp signUp){

        connectionString= getConnection();
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand sqlcommand = new SqlCommand($"select Role from Userdetails where Userid='{signUp.userid}' and Password='{signUp.password}'",connection);
        string Role = Convert.ToString(sqlcommand.ExecuteScalar());
        connection.Close();
        if(Role.Equals("Employee")){
            return "Employee";
        }
        else if(Role.Equals("Admin")){
            return "Admin";}
        else {
            return "Invalid";
        }
    }

    public  bool Forgot(SignUp signup)
        {
            
            SqlConnection connection = new SqlConnection(getConnection());
            SqlCommand command=new SqlCommand($"Select count(Userid) from Userdetails where Userid='{signup.userid}'",connection);
            connection.Open();
            if(Convert.ToInt32(command.ExecuteScalar())==1)
            {
                SqlCommand command1=new SqlCommand($"Update Userdetails set Password = '{signup.password}' where Userid= '{signup.userid}';",connection);
                command1.ExecuteNonQuery();
                connection.Close();
                return true;

            }
            else
            {
                connection.Close();
                return false;
                            
            }
        }
        public static void Delete(int Userid)
        {
            connectionString= getConnection();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand($"delete from Userdetails where Userid='{Userid}'",connection);
            sqlcommand.ExecuteNonQuery();
            connection.Close();
            return;
        }
        
        public  void AddClaim(Claims claims)
        {
            connectionString= getConnection();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
             Byte[] Document = System.IO.File.ReadAllBytes("C:/Users/Gokul/Downloads/Nathiyaa_Insurance_KCT/24.07.23 Insurance/Insurance/wwwroot/pdf/"+claims.Document);
            SqlCommand sqlcommand = new SqlCommand($"insert into Claims values({claims.PolicyID},@document,'{claims.Status}')",connection);
            sqlcommand.Parameters.Add("@document",sqlDbType:System.Data.SqlDbType.VarBinary).Value = Document;
            sqlcommand.ExecuteNonQuery();
            connection.Close();
            return;
        }

          public  void UpdateClaim(int ClaimID,string Status)
        {
            connectionString= getConnection();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand($"update Claims set Status='{Status}' where ID={ClaimID}",connection); 
            sqlcommand.ExecuteNonQuery();
            connection.Close();
            return;
        }

public static List<Claims> ViewClaims(int UserId)
        {
            List<Claims> ClaimsList = new List<Claims>();
            
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                SqlDataAdapter sqlDataAdapter=new SqlDataAdapter($"select * from Claims,Policy where Claims.PolicyId=Policy.SNo and Policy.Userid={UserId}",connection);
                DataTable dataTable=new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Claims claims = new Claims();
                    claims.ClaimID=Convert.ToInt32(dataRow["ID"].ToString());
                    claims.PolicyID=Convert.ToInt32(dataRow["PolicyId"].ToString());
                    claims.Status=(dataRow["Status"].ToString());
                    ClaimsList.Add(claims);
                    
                }
                }
            
            }
            catch (SqlException e)
            {                  
                Console.WriteLine("Datebase error"+e);               
            }
            return ClaimsList;
        }
public static List<Claims> ViewClaims()
        {
            List<Claims> ClaimsList = new List<Claims>();
            
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                SqlDataAdapter sqlDataAdapter=new SqlDataAdapter($"select * from Claims where Status='On Process'",connection);
                DataTable dataTable=new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Claims claims = new Claims();
                    claims.ClaimID=Convert.ToInt32(dataRow["ID"].ToString());
                    claims.PolicyID=Convert.ToInt32(dataRow["PolicyId"].ToString());    
                    ClaimsList.Add(claims);
                    
                }
                }
            
            }
            catch (SqlException e)
            {                  
                Console.WriteLine("Datebase error"+e);               
            }
            return ClaimsList;
        }
        public byte[] ViewPDF(int ClaimID){

            SqlConnection connection = new SqlConnection(getConnection());
            SqlCommand sqlCommand = new SqlCommand($"select Proofdocument from Claims where ID='{ClaimID}';",connection);
            connection.Open();
            var Document = sqlCommand.ExecuteScalar();
            
            connection.Close();
            return Document as byte[];

        }

    public static string getConnection()
    {
       var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);

        IConfiguration configuration = build.Build();

        string connectionString = Convert.ToString(configuration.GetConnectionString("DB1"));

        return connectionString;
    }
}