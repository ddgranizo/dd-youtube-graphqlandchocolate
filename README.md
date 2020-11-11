# dd-youtube-graphqlandchocolate

Video en [Youtube](https://youtu.be/gOFziCMIyzM "Video en youtube")

En este video haremos una pequeña introducción a GraphQL y veremos como implementar desde cero un API con AspNet Core utilizando esta tecnología.
El modelo de datos lo crearemos con Entity Framework Core y como proveedor de repositorio utilizamos SQL Server.
En el video se muestran ejemplos utilizados por los desarrolladores de la librería Hot Chocolate, un API para NetCore que nos permite utilizar GraphQL en nuestras aplicaciones. Además utilizaremos su herramienta de peticiones llamada Banana Cake Pop.
Las operaciones de GraphQL que revisaremos son: Query, Mutation y Subscription

Comandos de EF utilizados en el video:
- Add-Migration InitialModel
- Update-Database

Queries utilizadas en el video:

## Queries
query{
  opportunitities{
    subject
    account{
      name
    }
    owner{
      email
      name
    }
  }
}

query{
  opportunitities (where: {subject: "Oportunidad 2"}) {
    subject
    account {
      name
    }
    owner{
      email
      name
    }
  }
}

## Mutations
mutation  {
  createOpportunity(
    accountId : "00553146-C836-49EB-AD87-A8C639A4F054"
     subject: "Oportunidad 4"
  ownerid: "f0c74a06-1849-4ad7-a28f-3981697c3e71"
  ) 
}

## Subscription

subscription{
  onMessages
}

subscription{
  onOpportunityCreated
}

