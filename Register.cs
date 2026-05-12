if (ModelState.IsValid)
{
    tenant.ProfilePicture = "/images/default.jpg";
    _context.Add(tenant);
    await _context.SaveChangesAsync();
}

/*First it checks if all the form fields passed validation through ModelState.IsValid. If valid, it sets a default profile picture then adds the tenant to the database and saves it.
Then right after saving the tenant, it automatically creates a rent transaction for them:
csharp */

Then right after saving the tenant, it automatically creates a rent transaction for them:

var newTenantRent = new Transaction
{
    TenantId = tenant.Id,
    Description = "Monthly Rent - May 2026",
    Amount = 15000,
    Date = DateTime.Now,
    Status = "Pending",
    Type = TransactionType.Rent
};

_context.Transactions.Add(newTenantRent);
await _context.SaveChangesAsync();
//It creates a Transaction object, assigns it to the newly registered tenant using their ID, sets the amount, status as Pending, then saves it to the database. So when the tenant logs in for the first time, they already have a transaction waiting.

Then at the end:

return RedirectToAction("Login");
//After successful registration, it redirects to the login page.
