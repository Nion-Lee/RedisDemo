using StackExchange.Redis;

var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
var db = redis.GetDatabase();

Console.Write("請輸入欲設定之key：");
var input = Console.ReadLine();

Console.Write(input + "值設定為：");
var value = Console.ReadLine();

await db.StringSetAsync(input, value);
Console.WriteLine("設定完成！\n");

Console.WriteLine("請點擊任一按鍵接續Pub/Sub範例--\n");
Console.ReadKey();

Console.Write("\n請輸入欲推播之主題：");
var topic = Console.ReadLine();


string? content;
var sub = redis.GetSubscriber();
while (true)
{
    Console.Write("請輸入推播內容：");
    content = Console.ReadLine();

    await sub.PublishAsync(topic, content);
}