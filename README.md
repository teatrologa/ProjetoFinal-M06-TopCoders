# Projeto Final M06 TopCoders - Construção de API - API de Eventos
### Enunciado do Projeto

Construa uma API que registre e manipule eventos que acontecem na cidade, como shows, peças de teatro, eventos especiais em restaurantes, entre outros.

Implemente a documentação completa da API, utilizando Swagger. Todos os métodos devem possuir validação dos campos obrigatórios, quais os formatos de dados que a API recebe e responde e quais os possíveis status de retorno.

Construa uma API bem estruturada, respeitando as diretrizes de REST, SOLID e os princípios base de arquitetura.

Trate as exceções que forem necessárias.

#### Esta API deverá ter um cadastro do evento e um cadastro de reservas. Siga a estrutura apresentada abaixo:
```
CityEvent:
IdEvent             long         incremento PK
Title               string       not null
Description         string       null
DateHourEvent       DateTime     not null
Local               string       not null
Address             string       null
Price               decimal      null
Status              bit          not null


EventReservation:
IdReservation       long        incremento PK
IdEvent             long        not null FK
PersonName          string      not null
Quantity            long        not null
```

#### Para o CityEvent, construa os métodos:
- Inclusão de um novo evento; *Autenticação e Autorização admin
- Edição de um evento existente, filtrando por id; *Autenticação e Autorização admin
- Remoção de um evento, caso o mesmo não possua reservas em andamento, caso possua inative-o; *Autenticação e Autorização admin
- Consulta por título, utilizando similaridades, por exemplo, caso pesquise Show, traga todos os eventos que possuem a palavra Show no título;
- Consulta por local e data;
- Consulta por range de preço e a data;

#### Para o EventReservation, construa os métodos:
- Inclusão de uma nova reserva; *Autenticação
- Edição da quantidade de uma reserva; *Autenticação e Autorização admin
- Remoção de uma reserva; *Autenticação e Autorização admin
- Consulta de reserva pelo PersonName e Title do evento, utilizando similaridade para o title; *Autenticação

#### Utilize para autenticação os seguintes parametros:
```
Audience = "APIEvents.com"
Issuer = "APIClientes.com"
key = "he0ErCNloe4J7Id0Ry2SEDg09lKkZkfsRiGsdX_vgEg"
```
