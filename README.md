# Crypto get certs
1) Загрузить все файлы в 1 папку;
2) Сбилдить приложение в папке app;
3) Загрузить расширение в браузер из папки extension (browser://extensions или chrome://extensions);
4) Взять id расширения из browser://extensions и поменять в host_manifest.json свойства в "allowed_origins";
5) В файле host_manifest.json в поле path изменить путь до exe приложения которое сбилдили. (app -> bin -> Debug -> netcoreapp3.1) (заменить \ на \\\)
6) Добавить раздел в реестр по пути HKEY_CURRENT_USER -> SOFTWARE -> Google -> Chrome -> NativeMessagingHosts с названием com.certs.native со значением пути до файла host_manifest.json
7) Поменять id расширения в app.js на хосте сайта
