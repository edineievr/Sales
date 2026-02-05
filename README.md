**Sales**

**O que é este projeto?**



Esse projeto existe como objeto de estudo para aprimorar conhecimentos em modelagem, elaborações de casos de uso, validações com testes unitários e desenvolvimento baseado em comportamento.



O projeto, atualmente, está divido em 2 camadas (Domain e Application). Foram aplicados conceitos baseados em DDD, Clean Architecture e Principios de SOLID.



**Estilo Arquitetural**



Dominio, a camada principal.

Foi desenhado de tal maneira que permite evolução continua e está totalmente desacoplado de camadas externas.



Application, o orquestrador de casos de uso.

Atualmente valida inputs com validator (que serão movidos para a camada de apresentação futuramente) e orquestra casos de uso simples como 'CreateOrder' e 'GetOrder'. Foi pensado para também permitir evolução continua e desenvolvimento com baixa fricção mental.



**Conceitos Principais**



O principal agregado é Order, nele contem o controle sobre suas próprias regras de negócios, assim como garantias de invariantes de entidades adjacentes.



Por exemplo, o gerenciamento de itens acontece todo pelo Entidade Order, garantindo a consistência de invariantes de domínio.



**Como Rodar**



Para rodar esse projeto, basta cloná-lo em uma IDE de sua escolha, executar o build da solução e rodar os testes unitários.



Através dos testes é possível verificar quais invariantes estão sendo testadas, assim como regras de negócio. Todos os testes refletem a verdade absoluta do código de domínio.



**Status**

**O que está implementado**



O projeto conta com 6 casos de uso simples juntamente testes unitários e testes de aplicação:



**CreateOrderUseCase**

Onde é possível criar um Order, onde a implementação foi feita através de command + handler.



**GetOrderUseCase**

Aqui é possível recuperar um Order através de sua identidade que é convertido e objeto de transferência para apresentação.



**CancelOrderUseCase**

Responsável por recuperar e cancelar um Order através da sua identidade



**ListOrdersUseCase**

Responsável pela listagem de Orders, futuramente será paginado.



**ApplyDiscountOrderUseCase**

Aqui temos um caso de uso onde é possível aplicar desconto em um Order



**RemoveDiscountOrderUseCase**

Responsável por orquestrar a remoção do desconto de um Order



**Unit Tests**



É onde todas as invariantes atuais estão sendo testadas e validadas. Os testes escritos foram pensados para refletirem o comportamento do domínio. A camada de testes se mantém atômica, não dependendo de camadas externas.



**Application Tests**



Camada responsável por validar os casos de uso atualmente implementados. Foi pensada para não depender de camadas externas e manter a idempotência.



**O que não está implementado**



**Camada de apresentação**

Futuramente será implementada a camada de apresentação, que contará com validações de input e transporte de informações através de view models, separando e mantendo contratos internos. Dependerá somente da camada Application.



**Camada de Infraestrutura**

Será a camada responsável pelo recuperação e escrita de dados. Será pensada para não depender de camadas externas, apenas do Dominio.

