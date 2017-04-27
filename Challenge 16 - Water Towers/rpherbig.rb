input = [5, 2, 3, 2, 1, 3]

water_line = [input[0], input[-1]].min

water_collected = 0
input[1..-2].each do |height|
  current_water_level = water_line - height
  water_collected += [current_water_level, 0].max
end

puts water_collected
