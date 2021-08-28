module Components.Navbar

open Lit.Feliz
open Types


let View (onBackRequested: _ -> unit) (goToPage: Page -> _ -> unit) =
  Html.nav [
    Html.button [
      Ev.onClick onBackRequested
      Html.text "Back"
    ]
    Html.button [
      Ev.onClick (goToPage Page.Home)
      Html.text "Home"
    ]
    Html.button [
      Ev.onClick (goToPage Page.Notes)
      Html.text "Notes"
    ]
  ]
