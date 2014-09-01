/// <reference path="../helpers/misc.ts" />
/// <reference path="../helpers/linq.ts" />

module Billboard.Models {
	
	export interface IRawCategoryModel
	{
		id: Guid;
		name: string;
		notes: IRawNoteModel[];
		noteCount: number;
	}

	export class CategoryModel implements IModel
	{
		public id: Guid;
		public name: KnockoutObservable<string>;
		public notes: KnockoutObservableArray<NoteModel>;
		public noteCount: KnockoutObservable<number>;
		
		constructor(raw: IRawCategoryModel) {
			this.id = raw.id;
			this.name = ko.observable(raw.name);
			this.notes = ko.observableArray($isUndefined(raw.notes) ? [] : raw.notes.select(r => new NoteModel(r)));
			this.noteCount = ko.observable(raw.noteCount);
		}
	}
	
	export interface IRawNoteModel
	{
		categoryId: Guid;
		id: Guid;
		title: string;
		message: string;
		price: number;
		email: string;
	}

	export class NoteModel implements IModel
	{
		public categoryId: Guid;
		public id: Guid;
		public title: KnockoutObservable<string>;
		public message: KnockoutObservable<string>;
		public price: KnockoutObservable<number>;
		public email: KnockoutObservable<string>;
		
		constructor(raw: IRawNoteModel) {
			this.categoryId = raw.categoryId;
			this.id = raw.id;
			this.title = ko.observable(raw.title);
			this.message = ko.observable(raw.message);
			this.price = ko.observable(raw.price);
			this.email = ko.observable(raw.email);
		}
	}
 
}
