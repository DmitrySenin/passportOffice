;(function() {
	angular
		.module('main')
		.factory("PersonalInfoLoader", PersonalInfoLoader);

	PersonalInfoLoader.$inject = ['$http', 'URLService'];

	function PersonalInfoLoader($http, URLService) {

		return {
			SearchingOptions : SearchingOptions,
			Load : Load
		};
		
		/**
		 * Create instance of object represents searching criteria.
		 * @return {Object} Object which fields contatins restirction of searching.
		 */
		function SearchingOptions() {
			this.FirstName = null;
			this.LastName = null;
			this.MiddleName = null;
			this.BirthdayDate = null;
			this.PassportSeries = null;
			this.PassportNumber = null;
		};

		/**
		 * Carries out request for getting information from passed URL.
		 * @param {Numeric} pageSize Count of records on one page.
		 * @param {String} pageNumber Number 
		 * @return {Promise}     Promise which represents flow of request.
		 */
		function Load(pageSize, pageNumber, searchingOptions) {
			return $http.get(URLService.BuildPersonalInfoGetURL(pageSize, pageNumber), { params : searchingOptions });
		};
	};
})();