//#define INIT_TESTS

#if !INIT_TESTS
const int clientNum = 100; // Numbers of clients in row, minimum = 1
int[] arrivalLimits = { 2, 6 }; // Limits of clients arriving in seconds. First - down limit, second - up limit.
int[] manageLimits = { 2, 6 }; //  Limits of clients managing in seconds. First - down limit, second - up limit.

Random random = new Random();
Calendar calendar = new Calendar();
Cashier cashier = new Cashier();
Queue queue = new Queue();
int arrivedClients = 0;
int currentClients = 0;
int sumWaitTime = 0;
int currentTime = 0;

void OnClientArrival(int currentTime)
{
    var client = new Client(currentTime);
    if (cashier.state == Cashier.State.Free)
    {
        cashier.SetClient(client);
        calendar.Add(2, currentTime + random.Next(manageLimits[0], manageLimits[1]));
    }
    else
        queue.Enqueue(client);
    if (++arrivedClients < clientNum)
        calendar.Add(1, currentTime + random.Next(arrivalLimits[0], arrivalLimits[1]));
}
void OnClientManaged(int currentTime)
{
    Client doneClient = new Client();
    cashier.FreeClient(ref doneClient);
    doneClient.LeaveTime = currentTime;

    Console.WriteLine("[{0}] Клиент пробыл в очереди {1} у.е.", ++currentClients, doneClient.LeaveTime - doneClient.ArriveTime);
    sumWaitTime += doneClient.LeaveTime - doneClient.ArriveTime;

    if (!queue.IsEmpty())
    {
        var client = new Client();
        queue.Pop(ref client);
        cashier.SetClient(client);
        calendar.Add(2, currentTime + random.Next(manageLimits[0], manageLimits[1]));
    }
}

calendar.Add(1, 0);
while (calendar.Size > 0 || !queue.IsEmpty())
{
    int calendar_id = 0, calendar_time = 0;
    calendar.Pop(ref calendar_id, ref calendar_time);
    currentTime = calendar_time;

    if (calendar_id == 1)
        OnClientArrival(currentTime);
    else if (calendar_id == 2)
        OnClientManaged(currentTime);
}
Console.WriteLine("Среднее время ожидания для " + clientNum + " клиентов составляет " + ((double)sumWaitTime / clientNum) + " у.е.");
#else
Queue queue = new Queue();
List<int> operations = new List<int>();

while(operations.Count < 50)
    operations.Add(new Random().Next(0,4));

foreach(var operation in operations)
{
    switch(operation)
    {
        case 0:
            queue.Enqueue(new Client());
            Console.WriteLine("[Enqueue] В очередь добавлен новый клиент");
            break;
        case 1:
            var client = new Client();
            if (queue.Pop(ref client) == true)
                Console.WriteLine("[Pop] Из очереди извлечен клиент");
            else
                Console.WriteLine("[Pop] Операция Pop вернула false. Очередь пуста");
            break;
        case 2:
            var clientd = new Client();
            if (queue.Peek(ref clientd) == true)
                Console.WriteLine("[Peek] Из очереди просмотрен передний объект");
            else
                Console.WriteLine("[Peek] Операция Peek вернула false. Очередь пуста");
            break;
        case 3:
            if (queue.IsEmpty() == true)
                Console.WriteLine("[IsEmpty] Метод вернул true. Очередь пуста");
            else
                Console.WriteLine("[IsEmpty] Операция Peek вернула false. Очередь не пуста");
            break;
    }
}
#endif