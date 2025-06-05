## Порядок установки
Для работы требуется установленный Directum RX версии 4.9 и выше.

### Установка для ознакомления
1. Склонировать репозиторий rx-template-structuredLogging в папку.
2. Указать в _ConfigSettings.xml DDS:
   ```xml
   <block name="REPOSITORIES">
     <repository folderName="Base" solutionType="Base" url="" />
     <repository folderName="RX" solutionType="Base" url="<адрес локального репозитория>" />
     <repository folderName="<Папка из п.1>" solutionType="Work" 
       url="https://github.com/TanaisCompany/rx-template-structuredLogging.git" />
   </block>
   ```

### Установка для использования на проекте
Возможные варианты:

**A. Fork репозитория**

1. Сделать fork репозитория rx-template-structuredLogging для своей учетной записи.
2. Склонировать созданный в п. 1 репозиторий в папку.
3. Указать в _ConfigSettings.xml DDS:
   ``` xml
   <block name="REPOSITORIES">
     <repository folderName="Base" solutionType="Base" url="" />
     <repository folderName="<Папка из п.2>" solutionType="Work"
       url="<Адрес репозитория gitHub учетной записи пользователя из п. 1>" />
   </block>
   ```

**B. Подключение на базовый слой.**

Вариант не рекомендуется, так как при выходе версии шаблона разработки не гарантируется обратная совместимость.
1. Склонировать репозиторий rx-template-integrationsettings в папку.
2. Указать в _ConfigSettings.xml DDS:
   ``` xml
   <block name="REPOSITORIES">
     <repository folderName="Base" solutionType="Base" url="" />
     <repository folderName="<Папка из п.1>" solutionType="Base"
       url="https://github.com/TanaisCompany/rx-template-structuredLogging.git" />
     <repository folderName="<Папка для рабочего слоя>" solutionType="Work"
       url="<Адрес репозитория gitHub>" />
   </block>
   ```

**C. Копирование репозитория в систему контроля версий.**

Рекомендуемый вариант для проектов внедрения.
1. В системе контроля версий с поддержкой git создать новый репозиторий.
2. Склонировать репозиторий rx-template-structuredLogging в папку с ключом `--mirror`.
3. Перейти в папку из п. 2.
4. Импортировать клонированный репозиторий в систему контроля версий командой: \
   `git push –mirror <Адрес репозитория из п. 1>`
