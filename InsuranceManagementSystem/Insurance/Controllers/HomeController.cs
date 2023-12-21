using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Insurance.Models;

namespace Insurance.Controllers;

public class HomeController : Controller
{
    public Repository repository=new Repository();
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    

    public IActionResult Index()
    {
        return View();
    }
     public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult nav()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(SignUp signUp)
    {
        string result = repository.login(signUp);
        if(result.Equals("Admin")){
            HttpContext.Session.SetString("UserId",Convert.ToString(signUp.userid));
            return View("nav");
        }
        else if (result.Equals("Employee")){
            HttpContext.Session.SetString("UserId",Convert.ToString(signUp.userid));
            return View("Insurance");
        }
        else{
            ViewBag.Message="Invalid credentails";
            return View();
        }
    }

    public IActionResult Insurance()
    {
        return View();
    }

   
    public IActionResult Logout()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }
     [HttpPost]
     public IActionResult ForgotPassword(SignUp signUp)
     {
        if(ModelState.IsValid)
        {
            if(repository.Forgot(signUp)){
                ViewBag.Message="Password Changed";
                return View("Login");
            }
            else{
                ViewBag.Message="Invalid User ID";
                return View();}
        }
        else
        {
            ViewBag.Message="Password does not match";
            return View();
        }

    }

    [HttpGet]
    public IActionResult Addemployees()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Addemployees(Add add)
    {
        repository.AddEmployee(add);
        return RedirectToAction("Employeedetails");
    }
    

    [HttpGet]
    public IActionResult Employeedetails()
    {
        List<Add> details=Repository.ViewEmployee();
        return View(details);
    }
    [HttpPost]
    public IActionResult Employeedetails(int Userid)
    {
        return RedirectToAction("Updatedetails",Repository.SeacrhEmployee(Userid));
    }
    
    [HttpGet]
    public IActionResult Updatedetails(Add add)
    {
        return View(add);
    }

    [HttpPost]
    public IActionResult Updatedetail(Add add)
    {
        Repository.Update(add);
        return RedirectToAction("Employeedetails");
    }

    [HttpPost]
    public IActionResult Delete(int userid)
    {
        Repository.Delete(userid);
        return RedirectToAction("Employeedetails");
    }
    
    [HttpGet]
    public IActionResult ApplyPolicy()
    {
        Policy policy= new Policy();
        policy.Userid=Convert.ToInt32(HttpContext.Session.GetString("UserId"));
        return View(policy);
    }
    
    [HttpPost]
    public IActionResult ApplyPolicy(Policy policy)
    {
        repository.AddPolicy(policy);
        return RedirectToAction("Subscribed");
    }

     [HttpGet]
    public IActionResult Subscribed()
    {
        
        return View(Repository.ApplingPolicy(Convert.ToInt32(HttpContext.Session.GetString("UserId"))));
    }
    [HttpPost]
    public IActionResult Subscribed(int ID)
    {
        Claims claims = new Claims();
        claims.PolicyID = ID;      
        return RedirectToAction("Claimpolicy","Home",claims);
    }
    [HttpGet]
    public IActionResult SubscribedDetails()
    {
        return View(Repository.ApplingPolicy());
    }
   
    [HttpGet]
    public IActionResult Search()
    {
        return View();
    
    }

    [HttpGet]
    public IActionResult Profile()
    {
        Add add=Repository.SeacrhEmployee(Convert.ToInt32(HttpContext.Session.GetString("UserId")));
        return View(add);
    }
    
    [HttpGet]
    public IActionResult PolicyDetails()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult PolicyDetails(SignUp signUp)
    {
       
        return View();
    }

    [HttpGet]
    public IActionResult Claimpolicy(Claims claims)
    {
        Console.WriteLine(claims.PolicyID);
        return View(claims);
    }
    
    [HttpPost]
    public IActionResult Claimspolicy(Claims claims)
    {
        claims.Status="On Process";
        repository.AddClaim(claims);
        return RedirectToAction("Claimdetails");
    }
    
    [HttpGet]
    public IActionResult Claimdetails()
    {
        return View(Repository.ViewClaims(Convert.ToInt32(HttpContext.Session.GetString("UserId"))));
    }
    public IActionResult ViewPDF(int claimsID)
    {
        byte[] Document = repository.ViewPDF(claimsID);
        return File(Document,"application/pdf");
    }
    // [HttpGet]
    //  public IActionResult ViewPDF(byte[] Document)
    // {
    //     return File(Document,"application/pdf");
    // }
    [HttpGet]
    public IActionResult Claimaccept()
    {
        return View(Repository.ViewClaims());
    }
    [HttpPost]
    public IActionResult Claimaccept(int ClaimID,string Status)
    {
        repository.UpdateClaim(ClaimID,Status);
        return RedirectToAction("Claimaccept");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
