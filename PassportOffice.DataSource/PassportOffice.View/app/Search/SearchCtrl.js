;(function() {
	'use strict';

	angular
		.module('main')
		.controller('SearchCtrl', SearchCtrl);

	SearchCtrl.$inject = ['PersonalInfoService', 'EventNames', '$rootScope'];

	function SearchCtrl(PersonalInfoService, EventNames, $rootScope) {
		var vm = this;

		vm.SearchingOptions = new PersonalInfoService.SearchingOptions();
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