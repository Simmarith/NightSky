require "pry"
require "json"
require "pathname"

data = IO.binread("./example-companies-permid.txt")
fake_file = Pathname.new("./data.json")

permids = data.split("\n").map {|x| x.split(",").last.split("1-").last }

similarity_data = permids.each_with_object({}) do |permid, hash|
  permids.each do |child_permid|
    next if permid == child_permid
    key = "#{permid}:#{child_permid}"
    hash[key] = rand
  end
end

company_data = {
  similarity_score: similarity_data
}

puts "Writing new fake data to: #{fake_file}"
IO.binwrite(fake_file, JSON.generate(company_data))
