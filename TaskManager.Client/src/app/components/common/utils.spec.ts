import { Utils } from "./utils";

describe('Utils methods tests', () => {

  it('getDateTimeRange should calculate date time range', () => {
      var startDate = new Date(2018, 9, 21, 9, 10, 10);
      var endDate = new Date(2018, 9, 23, 10, 40, 40);
      var range = Utils.getDateTimeRange(startDate, endDate);
      expect(range.days).toBe(2);
      expect(range.hours).toBe(1);
      expect(range.minutes).toBe(30);
      expect(range.seconds).toBe(30);
  });
});
