public IActionResult Logout()
{
    HttpContext.Session.Clear();
    return RedirectToAction("Login");
}

The simplest one. Session.Clear() wipes the session completely — the app forgets who was logged in. Then it sends the user back to the login page.
