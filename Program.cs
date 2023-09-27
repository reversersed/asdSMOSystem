const int clientNum = 10; // Numbers of clients in row, minimum = 1

Random random = new Random();
Calendar calendar = new Calendar();
Cashier cashier = new Cashier();
Queue queue = new Queue();
int currentClients = 0;
int sumWaitTime = 0;

//Initialization
int init_time = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 5;
calendar.Add(1, init_time);
while (calendar.Size < clientNum)
{
    init_time += random.Next(2, 6);
    calendar.Add(1, init_time);
}
//

while (calendar.Size > 0 || queue.IsEmpty() == false)
{
    int currentTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    int calendar_id = 0, calendar_time = 0;
    calendar.Peek(ref calendar_id, ref calendar_time);
    if(calendar_time-currentTime <= 0)
    {
        calendar.Pop(ref calendar_id, ref calendar_time);
        if(calendar_id == 1)
            queue.Enqueue(new Client(currentTime));
        else if(calendar_id == 2)
        {
            Client doneClient = new Client();
            cashier.FreeClient(ref doneClient);
            doneClient.LeaveTime = currentTime;
            Console.WriteLine("[{0}] Клиент пробыл в очереди {1} секунд(ы)", ++currentClients, doneClient.LeaveTime - doneClient.ArriveTime);
            sumWaitTime += doneClient.LeaveTime - doneClient.ArriveTime;
        }
    }

    if(queue.IsEmpty() == false && cashier.state == Cashier.State.Free)
    {
        Client newClient = new Client();
        queue.Pop(ref newClient);
        cashier.SetClient(newClient);
        calendar.Add(2, currentTime+random.Next(2, 6));
    }
}
Console.WriteLine("Среднее время ожидания для "+clientNum+" клиентов составляет " + ((double)sumWaitTime / clientNum) + " секунд(ы)");