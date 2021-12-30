Требуется реализовать сервис на asp core и консольное приложение, которое вызывает методы на сервисе.

Есть коллекция пользователей, у каждого пользователя есть: ID, Имя и Статус (New, Active, Blocked, Deleted).
Информация о пользователях хранится в БД MySql. При запуске сервиса нужно загрузить информацию из БД, затем раз в 10 минут обновлять данные. Считаем, что запросов на по-лучение данных много, а изменение данных происходит редко, поэтому при запросе UserInfo нужно брать данные из памяти, а не делать запрос в БД. 

Все исключения в сервисе должны обрабатываться в коде.
Методы, в путях которых есть /Auth/ должны использовать механизм Basic Authorization.

Внимание
Swagger - не подключать.
Не использовать memorycashe и его аналоги для хранения данных.


Описание  методов

Метод CreateUser
Создание пользователя.
Url: /Auth/CreateUser
POST
Входные параметры — XML вида:

<Request>
  <user Id="999" Name="alex">
    <Status>New</Status>
  </user>
</Request>

Ответ также в формате XML
Пример успешного ответа:

<Response Success="true" ErrorId="0">
  <user Id="999" Name="alex">
    <Status>New</Status>
  </user>
</Response>

Пример ответа при ошибке:
<Response Success="false" ErrorId="1">
  <ErrorMsg>User with id 99 already exist</ErrorMsg>
</Response>

Метод RemoveUser
Удаление пользователя.
POST
Url: /Auth/RemoveUser
Входные параметры — JSON вида:
{"RemoveUser":{"Id":999}}

Ответ:
успех: {"Msg": "User was removed","Success": true,"user":{"Status": “Deleted”,"Id": 999,"Name": "alex"}}

Ошибка:
{"ErrorId": 2,"Msg": "User not found","Success": false}

Метод UserInfo
Возвращает информацию о пользователе в виде html страницы
GET
Url: /Public/UserInfo?id=999

Метод SetStatus
Изменения статуса пользователя
POST
Url: /Auth/SetStatus
Content-Type:application/x-www-form-urlencoded

Параметры:

Название	Тип	Описание
Id	int	Ид пользователя
NewStatus	string	Статус пользователя


Формат ответа JSON

Название	Тип	Описание
Id	Int	Ид пользователя
Status	string	Статус пользователя
Name	String	Имя пользователя

