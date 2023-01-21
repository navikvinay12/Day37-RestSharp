using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text.Json.Nodes;

namespace RestSharpTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:4000");
        }
        private RestResponse GetEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees/GetAllEmployee", Method.Get);
            //Act
            RestResponse response = client.Execute(request);
            return response;
        }
        [TestMethod]
        public void OnCallingGetAPI_ReturnEmployeeList()    //counting no of employees from json file .
        {
            RestResponse response = GetEmployeeList();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            List<Employees> employeeList = JsonConvert.DeserializeObject<List<Employees>>(response.Content);
            Assert.AreEqual(8, employeeList.Count);
            foreach (Employees emp in employeeList)
            {
                Console.WriteLine(emp.id + "\t" + emp.first_name + "\t" + emp.last_name + "\t" + emp.email);
            }
        }
        //[TestMethod]
        public void OnCallingPostAPI_EmployeeGetsAdded()
        {
            RestRequest request = new RestRequest("/employees/AddEmployee", Method.Post);
            JsonObject jsonObjectbody = new JsonObject();
            jsonObjectbody.Add("first_name", "Git");
            jsonObjectbody.Add("last_name", "Hub");
            jsonObjectbody.Add("email", "GitHub@gmail.com");
            request.AddParameter("application/json", jsonObjectbody, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Employees employee = JsonConvert.DeserializeObject<Employees>(response.Content);
            Assert.AreEqual("Git", employee.first_name);
            Assert.AreEqual("Hub", employee.last_name);
            Assert.AreEqual("GitHub@gmail.com", employee.email);
        }
        [TestMethod]
        public void OnCallingPostAPI_MultileEmployeesAdded()
        {
            List<Employees> EmpList = new List<Employees>();
            EmpList.Add(new Employees("Pritesh","Shinde","ps@gmail.com"));
            EmpList.Add(new Employees("Ritesh", "Shinde", "rs@gmail.com"));
            foreach (Employees emp in EmpList)
            {
                RestRequest request = new RestRequest("/employees/AddEmployee", Method.Post);
                JsonObject jsonObjectbody = new JsonObject();
                jsonObjectbody.Add("First Name", emp.first_name);
                jsonObjectbody.Add("Last Name", emp.last_name);
                jsonObjectbody.Add("Email", emp.email);
                request.AddParameter("application/json", jsonObjectbody, ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }
        }
        [TestMethod]
        public void OnCallingPostAPI_ShallUpdate_ExistingEmployee()
        {   //arrange
            RestRequest request = new RestRequest("/employees/UpdateEmployee/5", Method.Put);
            JsonObject jsonObjectbody = new JsonObject();

            jsonObjectbody.Add("First Name", "compLusr");
            jsonObjectbody.Add("Last Name", "mgmtMgr");
            jsonObjectbody.Add("Email", "computermanagement@gmail.com");
            request.AddParameter("application/json", jsonObjectbody, ParameterType.RequestBody);
            //  act
            RestResponse response = client.Execute(request);
            // assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Employees employee = JsonConvert.DeserializeObject<Employees>(response.Content);
        }
        [TestMethod]
        public void GivenEmployeeIdOnDelete_ShouldReturnSucessStatus()
        {
            //arrange
            RestRequest request = new RestRequest("/employees/DeleteEmployee/10", Method.Delete);
            //act
            RestResponse response = client.Execute(request);
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
//json-server --port 4000 --routes customRoute222Batch.json --watch db222Batch.json