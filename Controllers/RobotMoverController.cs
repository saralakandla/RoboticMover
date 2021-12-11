using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API_Core_3._0.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API_Core_3._0.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
     public class RobotMoverController : ControllerBase
    {
        private readonly ILogger<RobotMoverController> _logger;
        public RobotMoverController(ILogger<RobotMoverController> logger)
        {
            _logger = logger;
        }
        [HttpPost("Payload")]
        public async Task<IActionResult> PayloadAsync(Load loadDto)
        {
            try
            {
                if (loadDto.loadid == 0 && loadDto.x == 0 && loadDto.y == 0) { 
                    throw new Exception("Body should not be null"); 
                }
                List<Robot> robots = null;
                List<CloseRobot> lstClosedRobots = new List<CloseRobot>();
                CloseRobot closestRobot = new CloseRobot(); ;
                using (var client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync("https://60c8ed887dafc90017ffbd56.mockapi.io/robots");

                    if (response.IsSuccessStatusCode)
                    {

                        var content = await response.Content.ReadAsStringAsync();
                        robots = JsonConvert.DeserializeObject<List<Robot>>(content);
                        foreach(Robot r in robots)
                        {
                            int x1 = loadDto.x, y1 = loadDto.y, x2 = r.x, y2 = r.y;
                            
                            double dblDistance = Math.Sqrt(Math.Pow(x2 - x1, 2) +
                                                        Math.Pow(y2 - y1, 2) * 1.0);

                            dblDistance = Math.Round(dblDistance * 100000.0) / 100000.0;
                            CloseRobot objRobot = new CloseRobot();
                            objRobot.robotId = r.robotId;
                            objRobot.distanceToGoal = dblDistance;
                            objRobot.batteryLevel = r.batteryLevel;
                            lstClosedRobots.Add(objRobot);
                        }

                    }
                }
                if (lstClosedRobots.Count > 0) {
                    lstClosedRobots = lstClosedRobots.OrderBy(rbt => rbt.distanceToGoal).Take(10).ToList();
                    closestRobot = lstClosedRobots.OrderByDescending(rbt => rbt.batteryLevel).FirstOrDefault();


                }
                return Ok(closestRobot);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error while getting robots Info : " + ex.Message);
                return Ok("Error while getting robots Info.Please check the request ");
            }
        }
       
       
    }
}