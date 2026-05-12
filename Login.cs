var tenantByRoom = await _context.Tenants
    .FirstOrDefaultAsync(t => t.RoomNumber == roomNumber);

//First it searches the database for a tenant matching the room number. FirstOrDefaultAsync means — give me the first match, or null if nothing found.
Then it checks three things one by one:
///////////////////////////////////////////////////////////////
if (tenantByRoom == null)
{
    ViewBag.RoomError = "Room Number not found.";
    return View();
}

if (tenantByRoom.Email != email)
{
    ViewBag.EmailError = "Email does not match this room.";
    return View();
}

if (tenantByRoom.Password != password)
{
    ViewBag.PasswordError = "Incorrect password.";
    return View();
}

Each check has its own specific error message sent back to the view through ViewBag. If any check fails, it stops immediately and returns the error — it doesn't proceed to the next check.
If all three pass:
//////////////////////////////////////////////////////
HttpContext.Session.SetInt32("TenantId", tenantByRoom.Id);
return RedirectToAction("Index", "Dashboard");

It saves the tenant's ID into the session — this is what keeps the user logged in. Then redirects to the dashboard.
