var dbEntry = await _context.Tenants.FindAsync(model.Id);

First it finds the existing tenant record in the database using their ID.
Then handles the profile picture if one was uploaded:
//////////////////////////////////////////////////////
if (ProfilePictureFile != null && ProfilePictureFile.Length > 0)
{
    string fileName = dbEntry.Id + "_profile" + 
        Path.GetExtension(ProfilePictureFile.FileName);
    
    string uploadPath = Path.Combine(
        Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

    using (var stream = new FileStream(uploadPath, FileMode.Create))
    {
        await ProfilePictureFile.CopyToAsync(stream);
    }

    dbEntry.ProfilePicture = "/uploads/" + fileName;
}

"It builds a unique filename using the tenant ID plus a timestamp — so it becomes something like 3_profile_638500000000.jpg. 
The timestamp is DateTime.Now.Ticks which gives the exact current time in very small units called ticks, 
making sure every upload produces a completely different filename even if the same tenant uploads multiple times. 
Then it saves the actual image file into the wwwroot/uploads folder. 
Then it stores only the file path in the database — not the image itself.
Then updates the text fields:
//////////////////////////////////////////////////////////
dbEntry.FirstName = model.FirstName;
dbEntry.MiddleName = model.MiddleName ?? "";
dbEntry.LastName = model.LastName;
dbEntry.ContactNumber = model.ContactNumber;

await _context.SaveChangesAsync();

Copies the new values from the form into the database record. The ?? "" on MiddleName 
means if it's left blank, store empty string instead of null. Then SaveChangesAsync commits everything to the database.
Then:
////////////////////////////////////////
return RedirectToAction("Index");
