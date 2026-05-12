var transaction = await _context.Transactions.FindAsync(transactionId);

if (transaction != null)
{
    transaction.Status = "Completed";
    transaction.Date = DateTime.Now;
    await _context.SaveChangesAsync();
}

It receives the transaction ID from the form. FindAsync searches the database for that specific transaction. 
If found, it changes the status from Pending to Completed and records the current date and time as the payment date. 
Then SaveChangesAsync commits that change permanently to the database.
