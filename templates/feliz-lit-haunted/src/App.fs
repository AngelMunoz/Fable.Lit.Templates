[<RequireQualifiedAccess>]
module App

open Feliz.Lit
open Fable.Haunted

open Types
open Components

let private app () =
  let page, setPage = Haunted.useState (Page.Home)

  let onBackRequested _ = printfn "Back requested"

  let goToPage page _ = setPage page

  let getPage page =
    match page with
    | Page.Home -> Html.custom ("flit-home", [])
    | Page.Notes -> Html.custom ("flit-notes", [])

  Html.article [
    Navbar.View onBackRequested goToPage
    Html.main [ getPage page ]
  ]
  |> toLit

let register () =
  defineComponent "flit-app" (Haunted.Component app)
