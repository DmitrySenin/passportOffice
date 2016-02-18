;(function() {
	'use strict';

	angular
		.module('main')
		.controller('SearchCtrl', SearchCtrl);

	SearchCtrl.$inject = ['PersonalInfoLoader', 'EventNames', '$rootScope'];

	function SearchCtrl(PersonalInfoLoader, EventNames, $rootScope) {
		var vm = this;

		vm.SearchingOptions = new PersonalInfoLoader.SearchingOptions();
		vm.NeedSort = false;

		// Represents datepicker's state.
		vm.dateprickerPopup = {
			opened: false
		};

		// Open datepicker.
		vm.openDatepickerPopup = function() {
			vm.dateprickerPopup.opened = true;
		};

		/**
		 * Start selecting of data using criteria.
		 */
		vm.StartSearch = function () {
			$rootScope.$emit(EventNames.Search, vm.SearchingOptions, vm.NeedSort);
		};
	}
})();