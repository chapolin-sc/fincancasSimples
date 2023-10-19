# fincancasSimples

Essa aplicação faz parte do meu portfólio de aplicações. Ela basicamente é um CRUD de produtos cosméticos e de beleza.
Apesar de simples, nesta aplicação eu uso vários conceitos de programação amplamente difundidos atualmente. Minha intenção
é atuar como desenvolvedor backend, então criei uma aplicação front apenas para que não desenvolvedores possam testar a aplicação,
mas deixei ela o mais simples possível, então já aviso que você não vai gostar do visual.
  Para o desenvolvimento foi usado o framework .Net Core C# da Microsoft, comecei resolvendo usar a arquitetura limpa
(Clean Architecture) que ganhou destaque nos últimos anos, criei uma camada de Domínio, defini nela as entidades que acabaram sendo
modelos de representação de banco de dados, o que me permitiu usar a abordagem Code First, uma abordagem onde eu crio classes
modelo definindo suas propriedades, relacionamentos etc e a partir desse modelo é criado o banco de dados.
