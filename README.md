# Delivery Service

## Тестовое задание

Консольное приложение для службы доставки, которое фильтрует заказы в зависимости от количества обращений в конкретном районе города и времени обращения с и по.

1. С клонируйте проект на свой компьютер

```sh
    git clone https://github.com/thebrokenblow/DeliveryService.git
```

2. Для запуска необходимо скачать .NET 8 SDK под вашу операционную систему
   https://dotnet.microsoft.com/en-us/download/dotnet/8.0

3. Далее для запуска проекта надо перейти в консоли в дерикторию проекта. В дериктории должно быть следующее содержимое
    ![Дериктория проекта](https://github.com/thebrokenblow/DeliveryService/blob/master/Photos/Console.png?raw=true)

4. Для запуска проекта нам необходимо использовать 5 параметров
```sh
    _cityDistrict - Район доставки
    _firstDeliveryDateTime - Время первой доставки
    _sourceOrder - путь до исходного файла (он есть в репозитории, рядом с файлом README.md под названием data.txt)
    _deliveryLog - путь до файла, куда будут записываться логи
    _deliveryOrder - путь до файла, куда будут записываться результат
```

5. Запускать приложение надо строго в том порядке в котором они описаны выше, так же значение параметров записываются в кавычках "" и между названием параметра и самим параметром ставится знак присваивания, без пробелов. Пример запуска приложения: 
```sh
dotnet run
_cityDistrict="Central"
_firstDeliveryDateTime="2023-12-10 10:40:00"
_sourceOrder="C:\Users\Artem\Desktop\data\data.txt"
_deliveryLog="C:\Users\Artem\Desktop\data\log.txt"
_deliveryOrder="C:\Users\Artem\Desktop\data\result.txt"
```