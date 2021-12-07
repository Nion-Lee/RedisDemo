using StackExchange.Redis;

var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
var db = redis.GetDatabase();

Console.Write("請輸入欲查詢之key：");
var input = Console.ReadLine();

var value = await db.StringGetAsync(input);
var result = string.IsNullOrEmpty(value.ToString()) ? "(X)該鍵值目前無對應資料" : value.ToString();

Console.WriteLine("其值為：" + result + "\n");

Console.WriteLine("請輸入欲訂閱之主題");
var topic = Console.ReadLine();

var sub = redis.GetSubscriber();
await sub.SubscribeAsync(topic, (_, message) =>
{
    Console.WriteLine($"來自主題{topic}推播之訊息：{(string)message}");
});

Console.WriteLine("已成功訂閱！");
Console.WriteLine("收播中...\n");

Console.ReadKey();