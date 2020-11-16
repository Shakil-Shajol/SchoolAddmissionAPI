using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [Route("TC")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadTC()
        {
            try
            {
                var file = Request.Form.Files[0];
                string ext = Path.GetExtension(file.FileName);
                string folderName = "";
                string pathToSave = "";
                string fileName = "";
                string dbPath = "";
                if (file.Length>0)
                {
                    if (ext == ".pdf")
                    {
                        folderName = Path.Combine("Files", "TC");
                        pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        fileName = Guid.NewGuid().ToString()+ext;
                        string fullPath = Path.Combine(pathToSave, fileName);
                        dbPath = Path.Combine(folderName, fileName);
                        using (var stream=new FileStream(fullPath,FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        return Ok(new { dbPath });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();

                }


            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error: {ex}");
            }
            
        }


        [Route("Image")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                string ext = Path.GetExtension(file.FileName);
                string folderName = "";
                string pathToSave = "";
                string fileName = "";
                string dbPath = "";
                if (file.Length > 0)
                {
                    if (ext == ".jpg"||ext == ".jpeg"||ext == ".png")
                    {
                        folderName = Path.Combine("Files", "Images");
                        pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        fileName = Guid.NewGuid().ToString()+ext;
                        string fullPath = Path.Combine(pathToSave, fileName);
                        dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        return Ok(new { dbPath });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();

                }


            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error: {ex}");
            }

        }
    }
}