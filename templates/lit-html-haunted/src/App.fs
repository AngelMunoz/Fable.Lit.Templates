[<RequireQualifiedAccess>]
module App

open Lit
open Haunted

open Types
open Components
open Browser.Types

let private app () =
  // use Haunted for component based functions
  // and Hook.useState for stateful functions that aren't components
  let page, setPage = Haunted.useState (Page.Home)

  let onBackRequested _ = printfn "Back requested"

  let goToPage (evt: CustomEvent<Page>) =
    let page =
      evt.detail |> Option.defaultValue Page.Home

    setPage page

  let getPage page =
    match page with
    | Page.Home -> html $"""<flit-home></flit-home>"""
    | Page.Notes -> html $"""<flit-notes></flit-notes>"""

  html
    $"""
        <article>
            <flit-navbar
              @go-to-page={goToPage}
              @go-back={onBackRequested}></flit-navbar>
            <main>{getPage page}</main>
        </article>
        """

let register () =
  defineComponent "flit-app" (Haunted.Component app)
