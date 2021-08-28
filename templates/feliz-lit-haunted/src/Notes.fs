[<RequireQualifiedAccess>]
module Pages.Notes

open Browser.Types
open Lit.Feliz
open Haunted

type private Note =
  { Id: int
    Title: string
    Body: string }

type private State =
  { CurrentNote: Note option
    Notes: Note list }

type private Msg =
  | Save
  | Remove of Note
  | SetTitle of string
  | SetBody of string

let private update state msg =
  match msg with
  | Save ->
    match state.CurrentNote
          |> Option.map
               (fun note ->
                 { note with
                     Id = (state.Notes |> Seq.length) + 1 }) with
    | Some note ->
      { state with
          Notes =
            { note with
                Id = state.Notes.Length + 1 }
            :: state.Notes }
    | None -> state

  | Remove note ->
    { state with
        Notes = state.Notes |> List.filter (fun n -> n <> note) }
  | SetTitle title ->
    let current =
      state.CurrentNote
      |> Option.map (fun current -> { current with Title = title })
      |> Option.orElse (Some { Id = 0; Title = ""; Body = "" })

    { state with CurrentNote = current }
  | SetBody body ->
    let current =
      state.CurrentNote
      |> Option.map (fun current -> { current with Body = body })
      |> Option.orElse (Some { Id = 0; Title = ""; Body = "" })

    { state with CurrentNote = current }

let private noteTemplate (note: Note) (index: int) =
  Html.li $"{note.Id} - {note.Title}" |> Feliz.toLit


let private view () =

  let (state, dispatch) =
    Haunted.useReducer (update, { CurrentNote = None; Notes = [] })

  Html.div [
    Html.form [
      Ev.onSubmit
        (fun ev ->
          ev.preventDefault ()
          dispatch Save)
      Html.input [
        Attr.typeText
        Attr.name "title"
        Attr.placeholder "Title"
        Ev.onKeyUp
          (fun evt ->
            SetTitle (evt.target :?> HTMLInputElement).value
            |> dispatch)
        Ev.onBlur
          (fun evt ->
            SetTitle (evt.target :?> HTMLInputElement).value
            |> dispatch)
      ]
      Html.input [
        Attr.typeText
        Attr.name "body"
        Attr.placeholder "Body"
        Ev.onKeyUp
          (fun evt ->
            SetBody (evt.target :?> HTMLInputElement).value
            |> dispatch)
        Ev.onBlur
          (fun evt ->
            SetBody (evt.target :?> HTMLInputElement).value
            |> dispatch)
      ]
      Html.button [
        Attr.typeSubmit
        Html.text "Add"
      ]
    ]
    Html.ul [
      Lit.LitHtml.repeat (
        state.Notes,
        (fun (note: Note) -> $"{note.Id}"),
        noteTemplate
      )
      :?> Lit.TemplateResult
      |> Feliz.ofLit
    ]
  ]
  |> Feliz.toLit

let register () =
  defineComponent
    "flit-notes"
    (Haunted.Component(view, {| useShadowDOM = false |}))
