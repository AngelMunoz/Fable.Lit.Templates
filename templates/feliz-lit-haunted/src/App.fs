[<RequireQualifiedAccess>]
module App

open Lit.Feliz
open Haunted

open Types
open Components

let private app () =
  let page, setPage = Haunted.useState (Page.Home)

  let onBackRequested _ = printfn "Back requested"

  let goToPage page _ = setPage page

  let getPage page =
    match page with
    | Page.Home -> Feliz.lit_html $"<flit-home></flit-home>"
    | Page.Notes -> Feliz.lit_html $"<flit-notes></flit-notes>"

  Html.article [
    Navbar.View onBackRequested goToPage
    Html.main [ getPage page ]
  ]
  |> Feliz.toLit

let register () =
  defineComponent
    "flit-app"
    (Haunted.Component(app, {| useShadowDOM = false |}))
