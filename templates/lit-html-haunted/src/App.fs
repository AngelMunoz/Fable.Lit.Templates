[<RequireQualifiedAccess>]
module App

open Lit
open Haunted

open Types
open Components

let private app () =
  // use Haunted for component based functions
  // and Hook.useState for stateful functions that aren't components
  let page, setPage = Haunted.useState (Page.Home)

  let onBackRequested _ = printfn "Back requested"

  let goToPage page _ = setPage page

  let getPage page =
    match page with
    | Page.Home -> html $"""<flit-home></flit-home>"""
    | Page.Notes -> html $"""<flit-notes></flit-notes>"""

  html
    $"""
        <article>
            {Navbar.View onBackRequested goToPage}
            <main>{getPage page}</main>
        </article>
        """

let register () =
  defineComponent "flit-app" (Haunted.Component app)
