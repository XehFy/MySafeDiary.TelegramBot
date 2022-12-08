# MySafeDiary
Идея проекта заключается в том, чтобы помочь пользователю вести дневник в телеграме, не опасаясь, что он будет прочитан из аккаунта.
Решается задача путем хранения всех записей удаленно от клиента телеграма. Для получения доступа к записям по одной (через календарь) или ко всем сразу (через пдф файл) необходимо вводить пароль. Добовляемые записи удаляются моментально автоматически (ровно как и вводимый каждый раз пароль для доступа), скрывая информацию которая была передана на хранение боту. А те записи, к которым пользователь получает доступ, легко удалить после прочтения (для автоматизации этого процесса достаточно включить автоматическую очистку диалога с ботом).
# Презентация работы бота
![](https://github.com/XehFy/MySafeDiary.TelegramBot/blob/master/MySafeDiary_presentation.gif)

## Диаграма классов на данный момент
![Скриншот 31-10-2022 180815](https://user-images.githubusercontent.com/94968044/199041206-fefe4f4f-c7ba-4f0d-a695-cf710957ab77.jpg)
(Дальнейшие разделы не содержат актуальной информации и требуют обновления, тк создавались в первый день работы над проектом, для того чтобы образно наметить задачи)
## ToDo

  <li>API телеграма не предоставляет возможности удалять сообщения пользователя, следовательно необходимо сообщить пользователю о необходимости настроить автоудаление самостоятельно
  <li>упрощенный вариант получения информации от пользователя, вместо ожидания ответа от пользователя после команды(например \пароль -> введите пароль -> ожидание сообщения) будет: пользователю высылается шаблон (например \password Пароль123), пользователь отправляет ответ в формате шаблона. 
  <li>при регистрации запрашивать eMail, в случае забытого пароля высылать его на почту

## Цепочка команд
![Скриншот 02-11-2022 022711](https://user-images.githubusercontent.com/94968044/199360964-3015d6e2-4b7c-4bd7-8f09-7c3cdef7ecb6.jpg)
